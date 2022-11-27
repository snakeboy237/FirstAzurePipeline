﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
    /// </summary>
    public interface IWithTempDataBuilder
    {
        /// <summary>
        /// Adds temp data entry to the built <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <param name="key">Key of the temp data entry.</param>
        /// <param name="value">Value of the temp data entry.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.</returns>
        IAndWithTempDataBuilder WithEntry(string key, object value);

        /// <summary>
        /// Adds temp data entries to the built <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <param name="entries">Dictionary of temp data entries.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.</returns>
        IAndWithTempDataBuilder WithEntries(IDictionary<string, object> entries);

        /// <summary>
        /// Adds temp data entries to the built <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <param name="entries">Anonymous object of temp data entries.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.</returns>
        IAndWithTempDataBuilder WithEntries(object entries);
    }
}
