﻿namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using Contracts.And;
    using Contracts.Base;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base class for all test builders with response model.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public abstract class BaseTestBuilderWithResponseModel<TActionResult> 
        : BaseTestBuilderWithResponseModel, 
        IBaseTestBuilderWithActionResult<TActionResult>
        where TActionResult : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithResponseModel{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        protected BaseTestBuilderWithResponseModel(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public TActionResult ActionResult => this.TestContext.MethodResultAs<TActionResult>();
        
        public override object GetActualModel() 
            => (this.TestContext.MethodResult as ObjectResult)?.Value;

        public override Type GetModelReturnType()
        {
            if (this.TestContext.MethodResult is ObjectResult objectResult)
            {
                var declaredType = objectResult.DeclaredType;
                if (declaredType != null)
                {
                    return declaredType;
                }
            }

            return this.GetActualModel()?.GetType();
        }

        public override void ValidateNoModel()
        {
            if (this.GetActualModel() != null)
            {
                this.ThrowNewResponseModelAssertionException();
            }
        }

        /// <summary>
        /// When overridden in a derived class, it will be used to throw failed validation exception.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public abstract void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue);

        public ObjectResult GetObjectResult()
        {
            if (!(this.TestContext.MethodResult is ObjectResult objectResult))
            {
                throw new InvocationResultAssertionException(string.Format(
                    "{0} action result to inherit from ObjectResult and have response data, but it did not.",
                    this.TestContext.ExceptionMessagePrefix));
            }

            return objectResult;
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<TActionResult> assertions)
            => this.Passing<TActionResult>(assertions);

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<TActionResult, bool> predicate)
            => this.Passing<TActionResult>(predicate);

        /// <inheritdoc />
        public IAndTestBuilder PassingAs<TActionResultAs>(Action<TActionResultAs> assertions)
            => this.Passing(assertions);

        /// <inheritdoc />
        public IAndTestBuilder PassingAs<TActionResultAs>(Func<TActionResultAs, bool> predicate)
            => this.Passing(predicate);
        
        protected void WithNoModel<TExpectedActionResult>()
            where TExpectedActionResult : ActionResult
        {
            var actualResult = this.TestContext.MethodResult as TExpectedActionResult;
            if (actualResult == null || this.GetActualModel() != null)
            {
                this.ThrowNewResponseModelAssertionException();
            }
        }

        private void ThrowNewResponseModelAssertionException() 
            => throw new ResponseModelAssertionException(string.Format(
                "{0} to not have a response model but in fact such was found.",
                this.TestContext.ExceptionMessagePrefix));
    }
}
