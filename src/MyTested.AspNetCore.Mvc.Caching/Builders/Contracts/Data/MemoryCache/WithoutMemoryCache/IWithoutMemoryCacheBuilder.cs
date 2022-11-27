﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// Used for building mocked <see cref="IMemoryCache"/>.
    /// </summary>
    public interface IWithoutMemoryCacheBuilder
    {
        /// <summary>
        /// Remove cache entry to the mocked <see cref="IMemoryCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <returns>The same <see cref="IAndWithoutMemoryCacheBuilder"/>.</returns>
        IAndWithoutMemoryCacheBuilder WithoutEntry(object key);

        /// <summary>
        /// Remove cache entries to the mocked <see cref="IMemoryCache"/>.
        /// </summary>
        /// <param name="keys">Keys of the cache entries.</param>
        /// <returns>The same <see cref="IAndWithoutMemoryCacheBuilder"/>.</returns>
        IAndWithoutMemoryCacheBuilder WithoutEntries(IEnumerable<object> keys);

        /// <summary>
        /// Remove cache params to the mocked <see cref="IMemoryCache"/>.
        /// </summary>
        /// <param name="keys">Keys of the cache entries.</param>
        /// <returns>The same <see cref="IAndWithoutMemoryCacheBuilder"/>.</returns>
        IAndWithoutMemoryCacheBuilder WithoutEntries(params object[] keys);

        /// <summary>
        /// Clear all entries persisted into the <see cref="IMemoryCache"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndWithoutMemoryCacheBuilder"/>.</returns>
        IAndWithoutMemoryCacheBuilder WithoutAllEntries();
    }
}
