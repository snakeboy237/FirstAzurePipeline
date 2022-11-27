﻿namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
    /// </summary>
    public class TempDataTestBuilder : BaseDataProviderWithStringKeyTestBuilder<IAndTempDataTestBuilder>, IAndTempDataTestBuilder
    {
        internal const string TempDataName = "temp data";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TempDataTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public TempDataTestBuilder(ComponentTestContext testContext)
            : base(testContext, TempDataName)
        {
        }

        /// <summary>
        /// Gets the data provider test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndTempDataTestBuilder"/>.</value>
        protected override IAndTempDataTestBuilder DataProviderTestBuilder => this;

        /// <inheritdoc />
        public ITempDataTestBuilder AndAlso() => this;

        /// <summary>
        /// When overridden in derived class provides a way to built the data provider as <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns>Data provider as <see cref="IDictionary{TKey,TValue}"/>.</returns>
        protected override IDictionary<string, object> GetDataProvider() => this.TestContext.GetTempData();
    }
}
