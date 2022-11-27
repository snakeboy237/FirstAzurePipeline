﻿namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;

    /// <summary>
    /// Contains <see cref="Microsoft.EntityFrameworkCore.DbContext"/>
    /// extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderEntityFrameworkCoreWithoutExtensions
    {
        /// <summary>
        /// Remove entity from the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">
        /// Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.
        /// </param>
        /// <param name="entity">
        /// Remove entity from the registered <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            object entity)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutData(data => data
                    .WithoutEntity(entity));

        /// <summary>
        /// Remove values from the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">
        /// Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.
        /// </param>
        /// <param name="entities">
        /// Remove values from the registered <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<object> entities)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutData(data => data
                    .WithoutEntities(entities));

        /// <summary>
        /// Remove values from the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">
        /// Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.
        /// </param>
        /// <param name="entities">
        /// Remove values from the registered <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params object[] entities)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutData(data => data
                    .WithoutEntities(entities));

        /// <summary>
        /// Wipes all tables from <see cref="Microsoft.EntityFrameworkCore.DbContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">
        /// Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutData(data => data
                    .WithoutAllEntities());

        /// <summary>
        /// Remove values from the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> on the tested component or wipes all tables from it.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">
        /// Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.
        /// </param>
        /// <param name="dbContextBuilder">
        /// Action setting the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> by using <see cref="IWithDbContextBuilder"/>.</param>
        /// <returns></returns>
        public static TBuilder WithoutData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutDbContextBuilder> dbContextBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            dbContextBuilder(new WithoutDbContextBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }
    }
}
