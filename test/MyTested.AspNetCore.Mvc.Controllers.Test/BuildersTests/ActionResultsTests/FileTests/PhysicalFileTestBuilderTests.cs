﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.FileTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class PhysicalFileTestBuilderTests
    {
        [Fact]
        public void ShouldPassForTheShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<PhysicalFileResult>(actionResult);
                });
        }
    }
}
