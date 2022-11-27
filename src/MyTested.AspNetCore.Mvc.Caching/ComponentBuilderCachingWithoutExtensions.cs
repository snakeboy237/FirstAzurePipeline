﻿namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Builders.Base;

    /// <summary>
    /// Contains <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderCachingWithoutExtensions
    {

        /// <summary>
        /// Clear all entities from <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutMemoryCache(cache => cache.WithoutAllEntries());

        /// <summary>
        /// Remove given entity with key from <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="key">Key of the entity that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            object key)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutMemoryCache(cache => cache.WithoutEntry(key));

        /// <summary>
        /// Remove given entities from <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="keys">Keys of the entities that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<object> keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutMemoryCache(cache => cache.WithoutEntries(keys));

        /// <summary>
        /// Remove given entities from <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="keys">Keys of the entities that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params object[] keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutMemoryCache(cache => cache.WithoutEntries(keys));

        /// <summary>
        /// Remove entity or entities from <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="memoryCacheBuilder">Action setting the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> values by using <see cref="IWithMemoryCacheBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutMemoryCacheBuilder> memoryCacheBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            memoryCacheBuilder(new WithoutMemoryCacheBuilder(actualBuilder.TestContext.HttpContext.RequestServices));

            return actualBuilder.Builder;
        }
    }
}
