﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Base;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Exceptions;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Primitives;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void RouteDataShouldBePopulatedWhenRequestAndPathAreProvided()
        {
            MyApplication.StartsFrom<TestStartup>();

            MyController<MvcController>
                .Instance()
                .WithHttpRequest(req => req.WithPath("/Mvc/WithRouteData/1"))
                .Calling(c => c.WithRouteData(1))
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void RouteDataShouldBePopulatedWhenRequestAndPathAreProvidedForPocoController()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithHttpRequest(req => req.WithPath("/Mvc/WithRouteData/1"))
                .Calling(c => c.WithRouteData(1))
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionDescriptorForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.CustomControllerContext);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor);
                    Assert.Equal("OkResultAction", controller.CustomControllerContext.ActionDescriptor.ActionName);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithoutValidationShouldNotValidateTheRequestModelForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.ModelStateCheck(TestObjectFactory.GetRequestModelWithErrors()))
                .ShouldHave()
                .ValidModelState();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CallingShouldHaveValidModelStateWhenThereAreNoModelErrorsForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    var modelState = controller.CustomControllerContext.ModelState;

                    Assert.True(modelState.IsValid);
                    Assert.Empty(modelState.Values);
                    Assert.Empty(modelState.Keys);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void NormalIndexOutOfRangeExceptionShouldShowNormalMessageForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            Assert.Throws<InvocationAssertionException>(
                () =>
                {
                    MyController<FullPocoController>
                        .Instance()
                        .Calling(c => c.IndexOutOfRangeException())
                        .ShouldReturn()
                        .Ok();
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCallForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var actionResultTestBuilder = MyController<FullPocoController>
                .Instance()
                .Calling(c => c.AsyncOkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithNormalActionCallForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var actionResultTestBuilder = MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithNormalVoidActionCallForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var voidActionResultTestBuilder = MyController<FullPocoController>
                .Instance()
                .Calling(c => c.EmptyAction());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyAction");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithTaskActionCallForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var voidActionResultTestBuilder = MyController<FullPocoController>
                .Instance()
                .Calling(c => c.EmptyActionAsync());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyActionAsync");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CallingShouldPopulateModelStateWhenThereAreModelErrorsForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var requestModel = TestObjectFactory.GetRequestModelWithErrors();

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    var modelState = controller.CustomControllerContext.ModelState;

                    Assert.False(modelState.IsValid);
                    Assert.Equal(2, modelState.Values.Count());
                    Assert.Equal("Integer", modelState.Keys.First());
                    Assert.Equal("RequiredString", modelState.Keys.Last());

                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithAuthenticatedNotCalledShouldNotHaveAuthorizedUserForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound()
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    var controllerUser = controller.CustomHttpContext.User;

                    Assert.False(controllerUser.IsInRole("Any"));
                    Assert.Null(controllerUser.Identity.Name);
                    Assert.Null(controllerUser.Identity.AuthenticationType);
                    Assert.False(controllerUser.Identity.IsAuthenticated);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithCustomSetupForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithSetup(c =>
                {
                    c.PublicProperty = new object();
                })
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.PublicProperty);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithHttpContextSessionShouldThrowExceptionIfSessionIsNotRegisteredForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();

            MyController<FullPocoController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassForThe<HttpContext>(context =>
                {
                    Assert.Throws<InvalidOperationException>(() => context.Session);
                });
        }

        [Fact]
        public void WithHttpContextShouldPopulateCustomHttpContextForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "Custom";
            httpContext.Response.StatusCode = 404;
            httpContext.Response.ContentType = ContentType.ApplicationJson;
            httpContext.Response.ContentLength = 100;

            MyController<FullPocoController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassForThe<HttpContext>(setHttpContext =>
                {
                    Assert.Equal("Custom", setHttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<HttpResponseMock>(setHttpContext.Response);
                    Assert.Same(httpContext.Response.Body, setHttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, setHttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, setHttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, setHttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, setHttpContext.Items);
                    Assert.Same(httpContext.Features, setHttpContext.Features);
                    Assert.Same(httpContext.RequestServices, setHttpContext.RequestServices);
                    Assert.Same(httpContext.TraceIdentifier, setHttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, setHttpContext.User);
                });

            MyController<FullPocoController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.Equal("Custom", controller.CustomHttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<HttpResponseMock>(controller.CustomHttpContext.Response);
                    Assert.Same(httpContext.Response.Body, controller.CustomHttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, controller.CustomHttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, controller.CustomHttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, controller.CustomHttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, controller.CustomHttpContext.Items);
                    Assert.Same(httpContext.Features, controller.CustomHttpContext.Features);
                    Assert.Same(httpContext.RequestServices, controller.CustomHttpContext.RequestServices);
                    Assert.Same(httpContext.TraceIdentifier, controller.CustomHttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, controller.CustomHttpContext.User);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithHttpContextSetupShouldPopulateContextPropertiesForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithHttpContext(httpContext =>
                {
                    httpContext.Request.ContentType = ContentType.ApplicationOctetStream;
                })
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.Equal(ContentType.ApplicationOctetStream, controller.CustomHttpContext.Request.ContentType);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithRequestShouldNotWorkWithDefaultRequestActionForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithRequestShouldWorkWithSetRequestActionForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithHttpRequest(req => req.WithFormField("Test", "TestValue"))
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithRequestAsObjectShouldWorkWithSetRequestActionForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new HttpContextMock();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues> { ["Test"] = "TestValue" });

            MyController<FullPocoController>
                .Instance()
                .WithHttpRequest(httpContext.Request)
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void UsingUrlHelperInsideControllerShouldWorkCorrectlyForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithRouteData()
                .Calling(c => c.UrlAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel("/FullPoco/UrlAction"));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void UnresolvedRouteValuesShouldHaveFriendlyExceptionForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<FullPocoController>
                        .Instance()
                        .Calling(c => c.UrlAction())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel(""));
                },
                "Route values are not present in the method call but are needed for successful pass of this test case. Consider calling 'WithRouteData' on the component builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServicesForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.CustomHttpContext);
                    Assert.NotNull(controller.CustomActionContext);
                    Assert.NotNull(controller.CustomControllerContext);
                    Assert.NotNull(controller.CustomControllerContext.HttpContext);
                    Assert.NotNull(controller.CustomHttpContext.Request);
                    Assert.NotNull(controller.CustomHttpContext.Response);
                    Assert.NotNull(controller.CustomControllerContext.ModelState);
                    Assert.NotNull(controller.CustomHttpContext.User);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor);
                    Assert.NotNull(controller.CustomControllerContext.ValueProviderFactories);
                    Assert.Empty(controller.CustomControllerContext.ValueProviderFactories);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor.ActionConstraints);
                    Assert.Empty(controller.CustomControllerContext.ActionDescriptor.ActionConstraints);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor.AttributeRouteInfo);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor.BoundProperties);
                    Assert.Empty(controller.CustomControllerContext.ActionDescriptor.BoundProperties);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor.FilterDescriptors);
                    Assert.Empty(controller.CustomControllerContext.ActionDescriptor.FilterDescriptors);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor.Parameters);
                    Assert.Empty(controller.CustomControllerContext.ActionDescriptor.Parameters);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor.Properties);
                    Assert.Empty(controller.CustomControllerContext.ActionDescriptor.Properties);
                    Assert.NotNull(controller.CustomControllerContext.ActionDescriptor.RouteValues);
                    Assert.Empty(controller.CustomControllerContext.ActionDescriptor.RouteValues);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServices()
        {
            MyController<MvcController>
                .Instance()
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.HttpContext);
                    Assert.NotNull(controller.HttpContext.RequestServices);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.NotNull(controller.ControllerContext.HttpContext);
                    Assert.NotNull(controller.ViewBag);
                    Assert.NotNull(controller.ViewData);
                    Assert.NotNull(controller.TempData);
                    Assert.NotNull(controller.Request);
                    Assert.NotNull(controller.Response);
                    Assert.NotNull(controller.MetadataProvider);
                    Assert.NotNull(controller.ModelState);
                    Assert.NotNull(controller.ObjectValidator);
                    Assert.NotNull(controller.Url);
                    Assert.NotNull(controller.User);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor);
                    Assert.NotNull(controller.ControllerContext.ValueProviderFactories);
                    Assert.Empty(controller.ControllerContext.ValueProviderFactories);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.ActionConstraints);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.ActionConstraints);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.AttributeRouteInfo);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.BoundProperties);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.BoundProperties);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.FilterDescriptors);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.FilterDescriptors);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.Parameters);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.Parameters);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.Properties);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.Properties);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.RouteValues);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.RouteValues);
                });
        }

        [Fact]
        public void WithControllerContextShouldSetControllerContextForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controllerContext = new ControllerContext
            {
                ActionDescriptor = new ControllerActionDescriptor
                {
                    ActionName = "Test"
                }
            };

            MyController<FullPocoController>
                .Instance()
                .WithControllerContext(controllerContext)
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.CustomControllerContext);
                    Assert.Equal("Test", controller.CustomControllerContext.ActionDescriptor.ActionName);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithControllerContextSetupShouldSetCorrectControllerContextForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithControllerContext(controllerContext =>
                {
                    controllerContext.RouteData.Values.Add("testkey", "testvalue");
                })
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.CustomControllerContext);
                    Assert.True(controller.CustomControllerContext.RouteData.Values.ContainsKey("testkey"));
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        private void CheckActionResultTestBuilder<TActionResult>(
            IActionResultTestBuilder<TActionResult> actionResultTestBuilder,
            string expectedActionName)
        {
            this.CheckActionName(actionResultTestBuilder, expectedActionName);

            actionResultTestBuilder
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<OkResult>(actionResult);
                });
        }

        private void CheckActionName(IBaseTestBuilderWithInvokedAction testBuilder, string expectedActionName)
        {
            var actionName = (testBuilder as BaseTestBuilderWithInvokedAction)?.ActionName;

            Assert.NotNull(actionName);
            Assert.NotEmpty(actionName);
            Assert.Equal(expectedActionName, actionName);
        }
    }
}
