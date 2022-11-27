﻿namespace MyTested.AspNetCore.Mvc.Builders.Invocations
{
    using Base;
    using Contracts.Invocations;
    using Contracts.CaughtExceptions;
    using CaughtExceptions;
    using Internal;
    using Internal.TestContexts;
    using ShouldHave;
    using ShouldReturn;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the view component result which will be tested.
    /// </summary>
    /// <typeparam name="TInvocationResult">Result from invoked view component in ASP.NET Core MVC.</typeparam>
    public class ViewComponentResultTestBuilder<TInvocationResult> : BaseTestBuilderWithViewComponentResult<TInvocationResult>, 
        IViewComponentResultTestBuilder<TInvocationResult>
    {
        public ViewComponentResultTestBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IViewComponentShouldHaveTestBuilder<TInvocationResult> ShouldHave()
        {
            InvocationValidator.CheckForException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return new ViewComponentShouldHaveTestBuilder<TInvocationResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IShouldThrowTestBuilder ShouldThrow()
        {
            TestHelper.ExecuteTestCleanup();
            InvocationValidator.CheckForNullException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return new ShouldThrowTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IViewComponentShouldReturnTestBuilder<TInvocationResult> ShouldReturn()
        {
            TestHelper.ExecuteTestCleanup();
            InvocationValidator.CheckForException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return new ViewComponentShouldReturnTestBuilder<TInvocationResult>(this.TestContext);
        }
    }
}
