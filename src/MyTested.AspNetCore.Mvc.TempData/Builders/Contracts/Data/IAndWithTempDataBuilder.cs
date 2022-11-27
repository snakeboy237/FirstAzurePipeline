﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> builder.
    /// </summary>
    public interface IAndWithTempDataBuilder : IWithTempDataBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <returns>The same <see cref="IWithTempDataBuilder"/>.</returns>
        IWithTempDataBuilder AndAlso();
    }
}
