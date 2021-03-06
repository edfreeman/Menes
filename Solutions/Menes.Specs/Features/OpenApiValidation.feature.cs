﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.3.0.0
//      SpecFlow Generator Version:3.1.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Menes.Specs.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("OpenApiValidation")]
    public partial class OpenApiValidationFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "OpenApiValidation.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "OpenApiValidation", "\tIn order to diagnose bad requests or responses\r\n\tAs a developer\r\n\tI want to be a" +
                    "ble to define openapi validation rules in my schema", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validate object definition")]
        [NUnit.Framework.CategoryAttribute("perScenarioContainer")]
        [NUnit.Framework.TestCaseAttribute("1", "{ \"properties\": {\"foo\": {type: \"string\"}, \"bar\": { type: \"number\" } } }", "{ \"foo\": \"something\", \"bar\": 14 }", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("2", "{ \"type\": \"object\", \"properties\": {\"foo\": {type: \"string\"}, \"bar\": { type: \"numbe" +
            "r\" } } }", "{ \"foo\": \"something\", \"bar\": 14 }", "valid", null)]
        public virtual void ValidateObjectDefinition(string iD, string schemaFragment, string payloadJSON, string result, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "perScenarioContainer"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("ID", iD);
            argumentsOfScenario.Add("Schema Fragment", schemaFragment);
            argumentsOfScenario.Add("Payload JSON", payloadJSON);
            argumentsOfScenario.Add("Result", result);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate object definition", null, tagsOfScenario, argumentsOfScenario);
#line 7
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
 testRunner.Given(string.Format("the schema \'{0}\'", schemaFragment), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 9
 testRunner.And(string.Format("the payload \'{0}\'", payloadJSON), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
 testRunner.When("I validate the payload against the schema", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 11
 testRunner.Then(string.Format("the result should be {0}", result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validate anyOf allOf and oneOf")]
        [NUnit.Framework.CategoryAttribute("perScenarioContainer")]
        [NUnit.Framework.TestCaseAttribute("1", "{ \"anyOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "3", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("2", "{ \"anyOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "3.3", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("3", "{ \"anyOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "[3.3, \"henry\"]", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("4", "{ \"anyOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "true", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("5", "{ \"anyOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "false", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("6", "{ \"anyOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "{ \"foo\": \"something\", \"bar\": 14 }", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("7", "{ \"anyOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "\"A string\"", "invalid", null)]
        [NUnit.Framework.TestCaseAttribute("8", "{ \"oneOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "3", "invalid", null)]
        [NUnit.Framework.TestCaseAttribute("9", "{ \"oneOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "3.3", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("10", "{ \"oneOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "[3.3, \"henry\"]", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("11", "{ \"oneOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "true", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("12", "{ \"oneOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "false", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("13", "{ \"oneOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "{ \"foo\": \"something\", \"bar\": 14 }", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("14", "{ \"oneOf\": [{\"type\": \"object\"}, {\"type\": \"array\"}, {\"type\": \"boolean\"}, {\"type\": " +
            "\"integer\"}, {\"type\": \"number\"}] }", "\"A string\"", "invalid", null)]
        [NUnit.Framework.TestCaseAttribute("15", "{ \"allOf\": [{\"type\": \"integer\"}, {\"type\": \"number\"}] }", "3", "valid", null)]
        [NUnit.Framework.TestCaseAttribute("16", "{ \"allOf\": [{\"type\": \"integer\"}, {\"type\": \"number\"}] }", "3.3", "invalid", null)]
        [NUnit.Framework.TestCaseAttribute("17", "{ \"allOf\":[{\"type\":\"object\",\"required\":[\"petType\"],\"properties\":{\"petType\":{\"type" +
            "\":\"string\"}},\"discriminator\":{\"propertyName\":\"petType\"}},{\"type\":\"object\",\"prope" +
            "rties\":{\"name\":{\"type\":\"string\"}}}]}", "{ \"petType\": \"cat\", \"name\": \"misty\" }", "valid", null)]
        public virtual void ValidateAnyOfAllOfAndOneOf(string iD, string schemaFragment, string payloadJSON, string result, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "perScenarioContainer"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("ID", iD);
            argumentsOfScenario.Add("Schema Fragment", schemaFragment);
            argumentsOfScenario.Add("Payload JSON", payloadJSON);
            argumentsOfScenario.Add("Result", result);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate anyOf allOf and oneOf", null, tagsOfScenario, argumentsOfScenario);
#line 19
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 20
 testRunner.Given(string.Format("the schema \'{0}\'", schemaFragment), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 21
 testRunner.And(string.Format("the payload \'{0}\'", payloadJSON), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 22
 testRunner.When("I validate the payload against the schema", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 23
 testRunner.Then(string.Format("the result should be {0}", result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
