﻿namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithContentTypeResultInternal<TContentTypeResultTestBuilder>
        : IBaseTestBuilderWithActionResultInternal<TContentTypeResultTestBuilder>
        where TContentTypeResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
