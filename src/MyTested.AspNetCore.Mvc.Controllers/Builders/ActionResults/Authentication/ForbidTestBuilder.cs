﻿namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Authentication
{
    using Builders.Base;
    using Contracts.ActionResults.Authentication;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ForbidResult"/>.
    /// </summary>
    public class ForbidTestBuilder
        : BaseTestBuilderWithActionResult<ForbidResult>,
        IAndForbidTestBuilder,
        IBaseTestBuilderWithAuthenticationResultInternal<IAndForbidTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbidTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ForbidTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndForbidTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IForbidTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="ForbidResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue) 
            => throw new ForbidResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "forbid",
                propertyName,
                expectedValue,
                actualValue));
    }
}
