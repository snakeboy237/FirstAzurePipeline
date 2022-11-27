﻿namespace MyTested.AspNetCore.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Utilities.Validators;
    using Xunit;

    public class RuntimeBinderValidatorTests
    {
        [Fact]
        public void ValidateBindingShouldNotThrowExceptionWithValidPropertyCall()
        {
            var actionResultWithProperty = new OkObjectResult("Test");

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var value = (actionResultWithProperty as dynamic).Value;
                Assert.NotNull(value);
                Assert.Equal("Test", value);
            });
        }

        [Fact]
        public void ValidateBindingShouldThrowExceptionWithInvalidPropertyCall()
        {
            Test.AssertException<InvalidCallAssertionException>(
                () =>
                {
                    var actionResultWithProperty = new OkObjectResult("Test");

                    RuntimeBinderValidator.ValidateBinding(() =>
                    {
                        var value = (actionResultWithProperty as dynamic).InvalidValue;
                        Assert.NotNull(value);
                        Assert.Equal("Test", value);
                    });
                }, 
                "Expected object to contain 'InvalidValue' property, but in fact such property was not found.");
        }
    }
}
