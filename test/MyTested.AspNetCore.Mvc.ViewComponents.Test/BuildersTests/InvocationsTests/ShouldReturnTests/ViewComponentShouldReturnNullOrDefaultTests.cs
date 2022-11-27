﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentShouldReturnNullOrDefaultTests
    {
        [Fact]
        public void ShouldReturnNullShouldNotThrowExceptionWhenReturnValueNull()
        {
            MyViewComponent<NullComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Null();
        }

        [Fact]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueNotNull()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Null();
                },
                "When invoking NormalComponent expected view component result to be null, but instead received IViewComponentResult.");
        }

        [Fact]
        public void ShouldReturnNotNullShouldNotThrowExceptionWhenReturnValueNotNull()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .NotNull();
        }
        
        [Fact]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueNotNull()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<NullComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .NotNull();
                },
                "When invoking NullComponent expected view component result to be not null, but it was String object.");
        }

        [Fact]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIDefaultForClass()
        {
            MyViewComponent<NullComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .DefaultValue();
        }
        
        [Fact]
        public void ShouldReturnDefaultShouldThrowExceptionWhenReturnValueIsNotDefault()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .DefaultValue();
                },
                "When invoking NormalComponent expected view component result to be the default value of IViewComponentResult, but in fact it was not.");
        }
    }
}
