﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnChallengeTests
    {
        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionIfResultIsChallenge()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeResultAction())
                .ShouldReturn()
                .Challenge();
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionIfResultIsNotChallenge()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Challenge();
                },
                "When calling BadRequestAction action in MvcController expected result to be ChallengeResult, but instead received BadRequestResult.");
        }
    }
}
