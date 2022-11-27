﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnContentTests
    {
        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithNegotiatedContentResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(result => result
                    .WithContent("content"));
        }

        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithMediaTypeContentResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content(result => result
                    .WithContent("content"));
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithIncorrectContent()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ContentAction())
                        .ShouldReturn()
                        .Content(result => result
                            .WithContent("incorrect"));
                },
                "When calling ContentAction action in MvcController expected content result to contain 'incorrect', but instead received 'content'.");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithBadRequestResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Content(result => result
                            .WithContent("content"));
                },
                "When calling BadRequestAction action in MvcController expected result to be ContentResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnContentWithPredicateShouldNotThrowExceptionWithValidPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(result => result
                    .WithContent(content => content
                        .StartsWith("con")));
        }
        
        [Fact]
        public void ShouldReturnContentWithPredicateShouldThrowExceptionWithInvalidPredicate()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ContentAction())
                        .ShouldReturn()
                        .Content(result => result
                            .WithContent(content => content
                                .StartsWith("invalid")));
                },
                "When calling ContentAction action in MvcController expected content result ('content') to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnContentWithAssertionsShouldNotThrowExceptionWithValidPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(result => result
                    .WithContent(content =>
                    {
                        Assert.StartsWith("con", content);
                    }));
        }
    }
}
