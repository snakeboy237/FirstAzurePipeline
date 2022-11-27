﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ForbidTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class ForbidTestBuilderTests
    {
        [Fact]
        public void ShouldPassForTheShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidResultAction())
                .ShouldReturn()
                .Forbid()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ForbidResult>(actionResult);
                });
        }
    }
}
