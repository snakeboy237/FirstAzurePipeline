﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentTestBuilderTests
    {
        [Fact]
        public void NoAttributesShouldNotThrowExceptionWithControllerContainingNoAttributes()
        {
            MyViewComponent<NormalComponent>
                .ShouldHave()
                .NoAttributes();
        }

        [Fact]
        public void NoAttributesShouldThrowExceptionWithControllerContainingAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .ShouldHave()
                        .NoAttributes();
                },
                "When testing AttributesComponent was expected to not have any attributes, but it had some.");
        }

        [Fact]
        public void AttributesShouldNotThrowExceptionWithControllerContainingAttributes()
        {
            MyViewComponent<AttributesComponent>
                .ShouldHave()
                .Attributes();
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithControllerContainingNoAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .ShouldHave()
                        .Attributes();
                },
                "When testing NormalComponent was expected to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void AttributesShouldNotThrowExceptionWithControllerContainingNumberOfAttributes()
        {
            MyViewComponent<AttributesComponent>
                .ShouldHave()
                .Attributes(withTotalNumberOf: 2);
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithControllerContainingNumberOfAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 10);
                },
                "When testing AttributesComponent was expected to have 10 attributes, but in fact found 2.");
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithControllerContainingNumberOfAttributesTestingWithOne()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 1);
                },
                "When testing AttributesComponent was expected to have 1 attribute, but in fact found 2.");
        }
    }
}
