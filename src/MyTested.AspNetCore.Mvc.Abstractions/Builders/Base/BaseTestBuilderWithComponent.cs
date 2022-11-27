﻿namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using And;
    using Contracts.And;
    using Contracts.Base;
    using Exceptions;
    using Internal;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with component.
    /// </summary>
    public abstract class BaseTestBuilderWithComponent : BaseTestBuilder, IBaseTestBuilderWithComponent
    {
        private ComponentTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithComponent"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithComponent(ComponentTestContext testContext)
            : base(testContext) 
            => this.TestContext = testContext;

        /// <summary>
        /// Gets the currently used <see cref="ComponentTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ComponentTestContext"/>.</value>
        public new ComponentTestContext TestContext
        {
            get => this.testContext;

            private set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                this.testContext = value;
            }
        }
        
        /// <inheritdoc />
        public IAndTestBuilder ShouldPassForThe<TComponent>(Action<TComponent> assertions)
            where TComponent : class
        {
            this.TestContext.ComponentBuildDelegate?.Invoke();

            assertions(TestHelper.TryGetShouldPassForValue<TComponent>(this.TestContext));

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder ShouldPassForThe<TComponent>(Func<TComponent, bool> predicate)
            where TComponent : class
        {
            this.TestContext.ComponentBuildDelegate?.Invoke();

            if (!predicate(TestHelper.TryGetShouldPassForValue<TComponent>(this.TestContext)))
            {
                throw new InvalidAssertionException($"Expected {typeof(TComponent).ToFriendlyTypeName()} to pass the given predicate but it failed.");
            }

            return new AndTestBuilder(this.TestContext);
        }
    }
}
