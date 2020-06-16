﻿// <copyright file="Program.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Menes.OpenApiBuild
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.FileSystemGlobbing;
    using Microsoft.OpenApi;
    using Microsoft.OpenApi.Extensions;
    using Microsoft.OpenApi.Models;
    using Microsoft.OpenApi.Readers;
    using Microsoft.OpenApi.Services;
    using Microsoft.OpenApi.Validations;

    /// <summary>
    /// The main program.
    /// </summary>
    internal static class Program
    {
#pragma warning disable RCS1213 // Remove unused member declaration.
        /// <summary>
        /// Merges external component references into OpenApi documents.
        /// </summary>
        /// <param name="input">The input filespec.</param>
        /// <param name="output">The output directory.</param>
        /// <param name="root">The root folder in which to look for files (default: ./output).</param>
        /// <param name="console">The abstraction for the console.</param>
        /// <exception cref="ArgumentNullException"><paramref name="output"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <c>null</c>.</exception>
        private static void Main(string input, DirectoryInfo output, DirectoryInfo? root, IConsole? console = null)
        {
            if (input == null)
            {
                InvokeHelp();
                return;
            }

            if (output == null)
            {
                InvokeHelp();
                return;
            }

            root ??= new DirectoryInfo("./output");

            try
            {
                var matcher = new Matcher();
                matcher.AddInclude(input);
                foreach (string result in matcher.GetResultsInFullPath(root.FullName))
                {
                    var currentInput = new FileInfo(result);

                    OpenApiDocument openApiDocument = LoadOpenApiDocument(currentInput);

                    MergeExternalReferences(openApiDocument, currentInput.Directory);

                    ThrowOnError(openApiDocument.Validate(ValidationRuleSet.GetDefaultRuleSet()));

                    SaveOpenApiDocument(openApiDocument, new FileInfo(Path.Combine(output.FullName, currentInput.Name)));
                }
            }
            catch (Exception ex)
            {
                console?.Error.Write(ex.Message);
            }
        }

        private static void InvokeHelp()
        {
            System.CommandLine.DragonFruit.CommandLine.ExecuteAssembly(typeof(AutoGeneratedProgram).Assembly, new string[] { "--help" }, string.Empty);
        }
#pragma warning restore RCS1213 // Remove unused member declaration.

        private static void SaveOpenApiDocument(OpenApiDocument openApiDocument, FileInfo output)
        {
            // Ensure the directory exists
            output.Directory.Create();

            using var textWriter = new StreamWriter(output.Open(FileMode.Create, FileAccess.Write, FileShare.None));
            textWriter.Write(openApiDocument.SerializeAsYaml(OpenApiSpecVersion.OpenApi3_0));
        }

        private static void MergeExternalReferences(OpenApiDocument openApiDocument, DirectoryInfo directory)
        {
            var walker = new OpenApiWalkerEx(new MergeReferencesVisitor(openApiDocument, directory));

            // First walk loads everything, recursively
            walker.Walk(openApiDocument);
        }

        private static OpenApiDocument LoadOpenApiDocument(FileInfo input)
        {
            OpenApiDocument openApiDocument;
            using (StreamReader? textReader = input.OpenText())
            {
                var reader = new OpenApiTextReaderReader();
                openApiDocument = reader.Read(textReader, out OpenApiDiagnostic diagnostic);

                ThrowOnError(diagnostic.Errors);
            }

            return openApiDocument;
        }

        private static void ThrowOnError(IEnumerable<OpenApiError> errors)
        {
            if (errors.Any())
            {
                throw new Exception(string.Join(Environment.NewLine, errors.Select(e => e.Message)));
            }
        }
    }
}
