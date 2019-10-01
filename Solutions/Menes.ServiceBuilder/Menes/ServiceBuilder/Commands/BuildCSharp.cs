﻿// <copyright file="BuildCSharp.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Menes.ServiceBuilder.Commands
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Command to build a service in CSharp, from an OpenAPI document.
    /// </summary>
    [Command(Name = "build-csharp", Description = "Build the csharp implementation of the service.", ThrowOnUnexpectedArgument = false, ShowInHelpText = true)]
    [HelpOption]
    internal class BuildCSharp
    {
        /// <summary>
        /// Gets or sets a value indicating the OpenAPI document to load.
        /// </summary>
        [Option(Description = "The path to the OpenAPI document to load (required)", LongName = "document", ShortName = "d")]
        [Required]
        [FileExists]
        public string OpenApiDocumentPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the csproj project file.
        /// </summary>
        [Option(Description = "The path to the csproj project file (required)", LongName = "project", ShortName = "p")]
        [Required]
        [FileExists]
        public string ProjectPath { get; set; }

        /// <summary>
        /// Gets or sets the root namespace to use for the output.
        /// </summary>
        [Required]
        [Option(Description = "The root namespace to use for the output (required)", LongName = "namespace", ShortName = "n")]
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the default serialization casing.
        /// </summary>
        /// <remarks>
        /// <para>
        /// By default, we assume <c>camelCase</c> serialization of method property names.
        /// </para>
        /// <para>
        /// If you specify this option, it will used C# <c>IdentifierCase</c> (which will typically be PascalCase, but in
        /// practice is whatever the coding convention is that you have adopted.)
        /// </para>
        /// <para>
        /// This will be overridden by any <see cref="JsonProperty"/> declarations.
        /// </para>
        /// </remarks>
        [Option(Description = "Assume IdentifierCase serialization (as opposed to the default of camelCase)", LongName = "camelCase", ShortName = "c")]
        public bool DefaultToCamelCaseSerialization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Conventional form for command line application.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Conventional form for command line application.")]
        private async Task<int> OnExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
        {
            app.Out.WriteLine("I've built the output!");
            return await Task.FromResult(0).ConfigureAwait(false);
        }
    }
}
