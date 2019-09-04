﻿// <copyright file="OpenApiWebLink.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Menes.Links
{
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// Represents a hypermedia link to a related resource that's obtained from another OpenApi
    /// endpoint in the same service as the resource being returned. Extends the standard
    /// <see cref="WebLink"/> with <see cref="OperationId"/> and <see cref="OperationType" />
    /// properties which are useful internally when validating permissions for links.
    /// </summary>
    public class OpenApiWebLink : WebLink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiWebLink"/> struct.
        /// </summary>
        /// <param name="rel">The <see cref="WebLink.Rel"/>.</param>
        /// <param name="operationId">The <see cref="OperationId"/>.</param>
        /// <param name="href">The <see cref="WebLink.Href"/>.</param>
        /// <param name="operationType">The <see cref="OperationType"/>.</param>
        public OpenApiWebLink(string rel, string operationId, string href, OperationType operationType)
            : base(rel, href)
        {
            this.OperationType = operationType;
            this.OperationId = operationId;
        }

        /// <summary>
        /// Gets or sets the Http Method that this link is expected to be used with.
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Gets or sets the OpenApi OperationId that this link was generated from.
        /// </summary>
        public string OperationId { get; set; }

        /// <summary>
        /// Checks equality of two OpenApiWebLink instances.
        /// </summary>
        /// <param name="lhs">The left hand side of the expression.</param>
        /// <param name="rhs">The right hand side of the expression.</param>
        /// <returns>True if the two instances are equal, false otherwise.</returns>
        public static bool operator ==(OpenApiWebLink lhs, OpenApiWebLink rhs) => lhs.Equals(rhs);

        /// <summary>
        /// Checks inequality of two OpenApiWebLink instances.
        /// </summary>
        /// <param name="lhs">The left hand side of the expression.</param>
        /// <param name="rhs">The right hand side of the expression.</param>
        /// <returns>False if the two instances are equal, true otherwise.</returns>
        public static bool operator !=(OpenApiWebLink lhs, OpenApiWebLink rhs) => !lhs.Equals(rhs);

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is OpenApiWebLink link)
            {
                return (this.Href, this.Rel, this.OperationId, this.OperationType) == (link.Href, link.Rel, link.OperationId, link.OperationType);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (this.Href, this.Rel, this.OperationId, this.OperationType).GetHashCode();
        }
    }
}