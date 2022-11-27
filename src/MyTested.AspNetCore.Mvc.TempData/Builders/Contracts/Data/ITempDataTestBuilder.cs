﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
    /// </summary>
    public interface ITempDataTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> contains entry with the provided key.
        /// </summary>
        /// <param name="key">Key of the temp data entry.</param>
        /// <returns>The same <see cref="IAndTempDataTestBuilder"/>.</returns>
        IAndTempDataTestBuilder ContainingEntryWithKey(string key);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> contains entry with the provided value.
        /// </summary>
        /// <typeparam name="TValue">Type of the temp data entry value.</typeparam>
        /// <param name="value">Value of the temp data entry.</param>
        /// <returns>The same <see cref="IAndTempDataTestBuilder"/>.</returns>
        IAndTempDataTestBuilder ContainingEntryWithValue<TValue>(TValue value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> contains entry with value of the provided type.
        /// </summary>
        /// <typeparam name="TValue">Type of the temp data entry value.</typeparam>
        /// <returns>The same <see cref="IAndTempDataTestBuilder"/>.</returns>
        IAndTempDataTestBuilder ContainingEntryOfType<TValue>();

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> contains entry with value of the provided type and the given key.
        /// </summary>
        /// <typeparam name="TValue">Type of the temp data entry value.</typeparam>
        /// <param name="key">Key of the temp data entry.</param>
        /// <returns>The same <see cref="IAndTempDataTestBuilder"/>.</returns>
        IAndTempDataTestBuilder ContainingEntryOfType<TValue>(string key);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> contains entry with the provided key and corresponding value.
        /// </summary>
        /// <param name="key">Key of the temp data entry.</param>
        /// <param name="value">Value of the temp data entry.</param>
        /// <returns>The same <see cref="IAndTempDataTestBuilder"/>.</returns>
        IAndTempDataTestBuilder ContainingEntry(string key, object value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> contains specific entry by using a builder. 
        /// </summary>
        /// <param name="tempDataEntryTestBuilder">Builder for setting specific <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> entry tests.</param>
        /// <returns>The same <see cref="IAndTempDataTestBuilder"/>.</returns>
        IAndTempDataTestBuilder ContainingEntry(Action<IDataProviderEntryKeyTestBuilder> tempDataEntryTestBuilder);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> contains the provided entries. 
        /// </summary>
        /// <param name="entries">Anonymous object of temp data entries.</param>
        /// <returns>The same <see cref="IAndTempDataTestBuilder"/>.</returns>
        IAndTempDataTestBuilder ContainingEntries(object entries);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> contains the provided entries. 
        /// </summary>
        /// <param name="entries">Dictionary of temp data entries.</param>
        /// <returns>The same <see cref="IAndTempDataTestBuilder"/>.</returns>
        IAndTempDataTestBuilder ContainingEntries(IDictionary<string, object> entries);
    }
}
