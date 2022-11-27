﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnStatusCodeTests
    {
        [Fact]
        public void ShouldReturnHttpStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .StatusCode(result => result
                    .WithStatusCode(500));
        }

        [Fact]
        public void ShouldReturnHttpStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.StatusCodeAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .WithStatusCode(200));
                },
                "When calling StatusCodeAction action in MvcController expected status code result to have 200 (OK) status code, but instead received 500 (InternalServerError).");
        }
    }
}
