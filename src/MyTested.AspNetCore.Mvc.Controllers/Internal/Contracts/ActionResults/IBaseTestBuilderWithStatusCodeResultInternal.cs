﻿namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithStatusCodeResultInternal<TStatusCodeResultTestBuilder> 
        : IBaseTestBuilderWithActionResultInternal<TStatusCodeResultTestBuilder>
        where TStatusCodeResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
