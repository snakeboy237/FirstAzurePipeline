﻿namespace MyTested.AspNetCore.Mvc
{
    using Internal.Caching;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains caching extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionCachingExtensions
    {
        /// <summary>
        /// Replaces the default <see cref="IMemoryCache"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceMemoryCache(this IServiceCollection serviceCollection) 
            => serviceCollection.Replace<IMemoryCache, MemoryCacheMock>(ServiceLifetime.Transient);

        /// <summary>
        /// Replaces the default <see cref="IDistributedCache"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceDistributedCache(this IServiceCollection serviceCollection)
            => serviceCollection.Replace<IDistributedCache, DistributedCacheMock>(ServiceLifetime.Scoped);
    }
}
