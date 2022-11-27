﻿namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.ActionResults.View;
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.View;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Internal.Extensions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains <see cref="PartialViewResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderPartialViewResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PartialView<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder)
            => builder
                .PartialView(partialView => partialView
                    .WithDefaultName());

        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="partialViewTestBuilder">Builder for testing the partial view result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PartialView<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            Action<IPartialViewTestBuilder> partialViewTestBuilder)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;
            
            return actualBuilder.ValidateActionResult<PartialViewResult, IPartialViewTestBuilder>(
                partialViewTestBuilder,
                new PartialViewTestBuilder(actualBuilder.TestContext));
        }
    }
}
