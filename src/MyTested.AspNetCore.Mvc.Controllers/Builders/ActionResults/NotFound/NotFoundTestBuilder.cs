﻿namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.NotFound
{
    using System;
    using Builders.Base;
    using Contracts.ActionResults.NotFound;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing not found result.
    /// </summary>
    /// <typeparam name="TNotFoundResult">Type of not found result - <see cref="NotFoundResult"/> or <see cref="NotFoundObjectResult"/>.</typeparam>
    public class NotFoundTestBuilder<TNotFoundResult>
        : BaseTestBuilderWithResponseModel<TNotFoundResult>,
        IAndNotFoundTestBuilder,
        IBaseTestBuilderWithOutputResultInternal<IAndNotFoundTestBuilder>
        where TNotFoundResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public NotFoundTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the HTTP not found result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndNotFoundTestBuilder"/>.</value>
        public IAndNotFoundTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<NotFoundObjectResult> assertions)
        {
            this.ValidateNotFoundObjectResult();
            return this.Passing<NotFoundObjectResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<NotFoundObjectResult, bool> predicate)
        {
            this.ValidateNotFoundObjectResult();
            return this.Passing<NotFoundObjectResult>(predicate);
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder AndAlso() => this;

        public override void ValidateNoModel() => this.WithNoModel<NotFoundResult>();

        /// <summary>
        /// Throws new <see cref="NotFoundResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue) 
            => throw new NotFoundResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "not found",
                propertyName,
                expectedValue,
                actualValue));

        private void ValidateNotFoundObjectResult()
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(NotFoundObjectResult);

            if (actualResultType != expectedResultType)
            {
                throw new NotFoundResultAssertionException(string.Format(
                    "{0} not found result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
