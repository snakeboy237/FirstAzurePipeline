﻿namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authorization;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="AuthorizeAttribute"/>.
    /// </summary>
    public class AuthorizeAttributeTestBuilder : BaseAttributeTestBuilder<AuthorizeAttribute>,
        IAndAuthorizeAttributeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public AuthorizeAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext, nameof(AuthorizeAttribute), failedValidationAction)
            => this.Attribute = new AuthorizeAttribute();

        /// <inheritdoc />
        public IAndAuthorizeAttributeTestBuilder WithPolicy(string policy)
        {
            if (!string.IsNullOrEmpty(policy))
            {
                this.Attribute = new AuthorizeAttribute(policy)
                {
                    AuthenticationSchemes = this.Attribute.AuthenticationSchemes
                };

                this.Validations.Add((expected, actual) =>
                {
                    var expectedPolicy = expected.Policy;
                    var actualPolicy = actual.Policy;

                    if (!string.Equals(expectedPolicy, actualPolicy, StringComparison.OrdinalIgnoreCase))
                    {
                        this.FailedValidationAction(
                            $"{this.ExceptionMessagePrefix}'{expectedPolicy}' policy",
                            $"in fact found '{actualPolicy}'");
                    }
                });
            }

            return this;
        }

        /// <inheritdoc />
        public IAndAuthorizeAttributeTestBuilder WithRole(string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                this.Validations.Add((expected, actual) =>
                {
                    var actualRoles = new List<string>();

                    if (!string.IsNullOrEmpty(actual.Roles))
                    {
                        actualRoles = actual.Roles
                                        .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                        .ToList();
                    }

                    if (!actualRoles.Contains(role))
                    {
                        this.FailedValidationAction(
                            $"{this.ExceptionMessagePrefix}'{role}' role",
                            $"in fact such was not found");
                    }
                });
            }

            return this;
        }

        /// <inheritdoc />
        public IAndAuthorizeAttributeTestBuilder WithRoles(IEnumerable<string> roles)
        {
            if (roles != null && roles.Any())
            {
                roles.ForEach(role => this.WithRole(role));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndAuthorizeAttributeTestBuilder WithRoles(params string[] roles)
            => this.WithRoles(roles.AsEnumerable());

        /// <inheritdoc />
        public IAndAuthorizeAttributeTestBuilder WithAuthenticationSchemes(string authenticationSchemes)
        {
            if (!string.IsNullOrEmpty(authenticationSchemes))
            {
                this.Attribute.AuthenticationSchemes = authenticationSchemes;

                this.Validations.Add((expected, actual) =>
                {
                    var expectedAuthenticationSchemes = expected.AuthenticationSchemes;
                    var actualAuthenticationSchemes = actual.AuthenticationSchemes;

                    if (expectedAuthenticationSchemes != actualAuthenticationSchemes)
                    {
                        this.FailedValidationAction(
                            $"{this.ExceptionMessagePrefix}'{expectedAuthenticationSchemes}' authentication schemes",
                            $"in fact found '{actualAuthenticationSchemes}'");
                    }
                });
            }

            return this;
        }

        /// <inheritdoc />
        public IAuthorizeAttributeTestBuilder AndAlso() => this;
    }
}
