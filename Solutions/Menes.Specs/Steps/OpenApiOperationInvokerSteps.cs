﻿// <copyright file="OpenApiOperationInvokerSteps.cs" company="Endjin">
// Copyright (c) Endjin. All rights reserved.
// </copyright>

#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Elements should be documented

namespace Menes.Specs.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using Idg.AsyncTest;
    using Idg.AsyncTest.TaskExtensions;
    using Menes.Auditing;
    using Menes.Exceptions;
    using Menes.Internal;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Moq;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class OpenApiOperationInvokerSteps : IOpenApiService
    {
        private const string OperationInvokedScenarioContextKey = "OperationInvoked";

        private readonly Mock<IOpenApiService> openApiService = new Mock<IOpenApiService>();
        private readonly Mock<IOpenApiConfiguration> openApiConfiguration = new Mock<IOpenApiConfiguration>();
        private readonly Mock<IOpenApiContext> openApiContext = new Mock<IOpenApiContext>();
        private readonly Mock<IOpenApiServiceOperationLocator> operationLocator = new Mock<IOpenApiServiceOperationLocator>();
        private readonly Mock<IOpenApiAccessChecker> accessChecker = new Mock<IOpenApiAccessChecker>();
        private readonly Mock<IOpenApiExceptionMapper> exceptionMapper = new Mock<IOpenApiExceptionMapper>();
        private readonly Mock<IOpenApiResultBuilder<object>> resultBuilder = new Mock<IOpenApiResultBuilder<object>>();
        private readonly CompletionSourceWithArgs<CheckAccessArguments, IDictionary<AccessCheckOperationDescriptor, AccessControlPolicyResult>> accessCheckCalls = new CompletionSourceWithArgs<CheckAccessArguments, IDictionary<AccessCheckOperationDescriptor, AccessControlPolicyResult>>();
        private readonly OpenApiResult exceptionMapperResult = new OpenApiResult();
        private readonly object resultBuilderResult = new object();
        private readonly object resultBuilderErrorResult = new object();
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;
        private ResponseWhenUnauthenticated? responseWhenUnauthenticated;
        private OpenApiOperation openApiOperation;
        private OpenApiOperationPathTemplate operationPathTemplate;
        private Task<object> invokerResultTask;

        public OpenApiOperationInvokerSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        [Given("I have configured unauthenticated requests to produce (.*)")]
        public void GivenIAmHaveConfiguredTheUnauthenticatedStatusCodeToBe(string status)
        {
            if (status != "null")
            {
                this.responseWhenUnauthenticated = (ResponseWhenUnauthenticated)Enum.Parse(typeof(ResponseWhenUnauthenticated), status);
            }
        }

        [Given("the operation path template has an Operation with an operationId of '(.*)'")]
        public void GivenTheOperationPathTemplateHasAnOperationWithAnOperationIdOf(string operationId)
        {
            this.openApiOperation = new OpenApiOperation { OperationId = operationId };
            this.operationPathTemplate = new OpenApiOperationPathTemplate(this.openApiOperation, null);

            MethodInfo serviceMethod = typeof(OpenApiOperationInvokerSteps).GetMethod(
                nameof(this.ServiceMethodImplementation),
                BindingFlags.NonPublic|BindingFlags.Instance);

            var openApiServiceOperation = new OpenApiServiceOperation(this, serviceMethod, this.openApiConfiguration.Object);
            this.operationLocator
                .Setup(m => m.TryGetOperation(operationId, out openApiServiceOperation))
                .Returns(true);
        }

        [When("I handle a '(.*)' request for '(.*)'")]
        public void WhenIHandleARequestFor(string method, string path)
        {
            var parameterBuilder = new Mock<IOpenApiParameterBuilder<object>>();
            parameterBuilder
                .Setup(m => m.BuildParametersAsync(It.IsAny<object>(), this.operationPathTemplate))
                .Returns(Task.FromResult<IDictionary<string, object>>(new Dictionary<string, object>()));

            this.exceptionMapper
                .Setup(m => m.GetResponse(It.IsAny<Exception>(), It.IsAny<OpenApiOperation>()))
                .Returns(this.exceptionMapperResult);

            this.resultBuilder
                .Setup(m => m.BuildResult(It.IsAny<object>(), this.openApiOperation))
                .Returns(this.resultBuilderResult);
            this.resultBuilder
                .Setup(m => m.BuildErrorResult(It.IsAny<int>()))
                .Returns(this.resultBuilderErrorResult);

            var configuration = new OpenApiConfiguration(ContainerBindings.GetServiceProvider(this.featureContext));
            if (this.responseWhenUnauthenticated.HasValue)
            {
                configuration.AccessPolicyUnauthenticatedResponse = this.responseWhenUnauthenticated.Value;
            }

            var invoker = new OpenApiOperationInvoker<object, object>(
                this.operationLocator.Object,
                parameterBuilder.Object,
                this.accessChecker.Object,
                this.exceptionMapper.Object,
                this.resultBuilder.Object,
                configuration,
                new Mock<IAuditContext>().Object,
                new Mock<ILogger<OpenApiOperationInvoker<object, object>>>().Object);

            this.accessChecker
                .Setup(m => m.CheckAccessPoliciesAsync(It.IsAny<IOpenApiContext>(), It.IsAny<AccessCheckOperationDescriptor[]>()))
                .Returns((IOpenApiContext context, AccessCheckOperationDescriptor[] requests) => this.accessCheckCalls.GetTask(
                    new CheckAccessArguments { Context = context, Requests = requests }));

            this.invokerResultTask = invoker.InvokeAsync(
                path,
                method,
                new object(),
                this.operationPathTemplate,
                this.openApiContext.Object);
        }

        [When("the access checker blocks access with '(.*)'")]
        public async Task WhenTheAccessCheckerBlocksAccessWithoutAnExplanationAsync(AccessControlPolicyResultType resultType)
        {
            var result = new Dictionary<AccessCheckOperationDescriptor, AccessControlPolicyResult>
            {
                { this.accessCheckCalls.Arguments[0].Requests[0], new AccessControlPolicyResult(resultType) }
            };
            this.accessCheckCalls.SupplyResult(result);
            await this.WaitForInvokerToFinishIgnoringErrors().ConfigureAwait(false);
        }

        [When("the access checker blocks access with an explanation of '(.*)'")]
        public async Task WhenTheAccessCheckerBlocksAccessWithAnExplanationOfAsync(string explanation)
        {
            var result = new Dictionary<AccessCheckOperationDescriptor, AccessControlPolicyResult>
            {
                { this.accessCheckCalls.Arguments[0].Requests[0], new AccessControlPolicyResult(AccessControlPolicyResultType.NotAllowed, explanation) }
            };
            this.accessCheckCalls.SupplyResult(result);
            await this.WaitForInvokerToFinishIgnoringErrors().ConfigureAwait(false);
        }

        [When("the access checker allows access")]
        public async Task WhenTheAccessCheckerAllowsAccess()
        {
            var result = new Dictionary<AccessCheckOperationDescriptor, AccessControlPolicyResult>
            {
                { this.accessCheckCalls.Arguments[0].Requests[0], new AccessControlPolicyResult(AccessControlPolicyResultType.Allowed) }
            };
            this.accessCheckCalls.SupplyResult(result);
            await this.WaitForInvokerToFinishIgnoringErrors().ConfigureAwait(false);
        }

        [Then("the access checker should receive a path of '(.*)'")]
        public void ThenTheAccessCheckerShouldReceiveAPathOf(string path)
        {
            Assert.AreEqual(path, this.accessCheckCalls.Arguments[0].Requests[0].Path);
        }

        [Then("the access checker should receive an operationId of '(.*)'")]
        public void ThenTheAccessCheckerShouldReceiveAnOperationIdOf(string operationId)
        {
            Assert.AreEqual(operationId, this.accessCheckCalls.Arguments[0].Requests[0].OperationId);
        }

        [Then("the access checker should receive an HttpMethod of '(.*)'")]
        public void ThenTheAccessCheckerShouldReceiveAnHttpMethodOf(string method)
        {
            Assert.AreEqual(method, this.accessCheckCalls.Arguments[0].Requests[0].Method);
        }

        [Then("the access checker should receive the Open API context")]
        public void ThenTheAccessCheckerShouldReceiveTheOpenAPIContext()
        {
            Assert.AreSame(this.openApiContext.Object, this.accessCheckCalls.Arguments[0].Context);
        }

        [Then("the invoker should complete without exceptions")]
        public async Task ThenTheInvokerShouldCompleteWithoutExceptions()
        {
            await this.invokerResultTask.ConfigureAwait(false);
        }

        [Then("the operation method should not be invoked")]
        public void ThenTheOperationMethodShouldNotBeInvoked()
        {
            Assert.False(this.scenarioContext.ContainsKey(OperationInvokedScenarioContextKey));
        }

        [Then("the operation method should be invoked")]
        public void ThenTheOperationMethodShouldBeInvoked()
        {
            Assert.True(this.scenarioContext.ContainsKey(OperationInvokedScenarioContextKey));
        }

        [Then("the invoker should map an '(.*)' with no explanation")]
        public void ThenTheInvokerShouldMapAnOpenApiForbiddenExceptionWithNoExplanation(string exceptionType)
        {
            this.exceptionMapper.Verify(m => m.GetResponse(
                It.Is<Exception>(x => x.GetType().Name == exceptionType && !x.Data.Contains("detail")),
                this.openApiOperation));
        }

        [Then("the invoker should map an OpenApiForbiddenException with an explanation of '(.*)'")]
        public void ThenTheInvokerShouldMapAnOpenApiForbiddenExceptionWithAnExplanationOf(string explanation)
        {
            this.exceptionMapper.Verify(m => m.GetResponse(
                It.Is<Exception>(x => x is OpenApiForbiddenException && ((string)x.Data["detail"]) == explanation),
                this.openApiOperation));
        }

        [Then("the invoker should pass the method result to the result builder")]
        public void ThenTheInvokerShouldPassTheMethodResultToTheResultBuilder()
        {
            this.resultBuilder.Verify(m => m.BuildResult(
                this.scenarioContext[OperationInvokedScenarioContextKey],
                this.openApiOperation));
        }

        [Then("the invoker should pass the result from the exception mapper to the result builder")]
        public void ThenTheInvokerShouldPassTheResultFromTheExceptionMapperToTheResultBuilder()
        {
            this.resultBuilder.Verify(m => m.BuildResult(this.exceptionMapperResult, this.openApiOperation));
        }

        [Then("the invoker should return the result from the result builder")]
        public async Task ThenTheInvokerShouldReturnTheResultFromTheResultBuilder()
        {
            object result = await this.invokerResultTask;
            Assert.AreSame(this.resultBuilderResult, result);
        }

        [Then("invoker should return a (.*) error result")]
        public async Task ThenInvokerShouldReturnAErrorResult(int statusCode)
        {
            object result = await this.invokerResultTask;
            Assert.AreSame(this.resultBuilderErrorResult, result);
            this.resultBuilder.Verify(m => m.BuildErrorResult(statusCode));
        }

        private Task<OpenApiResult> ServiceMethodImplementation()
        {
            var result = new OpenApiResult { StatusCode = 200 };
            this.scenarioContext.Set(result, OperationInvokedScenarioContextKey);

            return Task.FromResult(result);
        }

        private async Task WaitForInvokerToFinishIgnoringErrors()
        {
            // We ignore errors, because some tests expect exceptions.
            await this.invokerResultTask.WhenCompleteIgnoringErrors().WithTimeout().ConfigureAwait(false);
        }

        private class CheckAccessArguments
        {
            public IOpenApiContext Context { get; set; }

            public AccessCheckOperationDescriptor[] Requests { get; set; }
        }
    }
}