﻿namespace MyTested.AspNetCore.Mvc.Test.InternalTests.RoutesTests
{
    using System.IO;
    using System.Reflection;
    using Internal.Application;
    using Internal.Http;
    using Internal.Routing;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Setups.Routing;
    using Xunit;
    using Setups;

    public class MvcRouteResolverTests
    {
        [Fact]
        public void ResolveShouldResolveCorrectlyWithPartialJsonContentBody()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithModel/5", "POST", body: @"{""Integer"":5}",
                    contentType: ContentType.ApplicationJson));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithModel", routeInfo.Action);
            Assert.Equal(2, routeInfo.ActionArguments.Count);
            Assert.Equal(5, routeInfo.ActionArguments["id"].Value);
            Assert.True(routeInfo.ActionArguments.ContainsKey("model"));

            var model = routeInfo.ActionArguments["model"].Value as RequestModel;

            Assert.NotNull(model);
            Assert.Equal(5, model.Integer);
            Assert.Null(model.String);

            Assert.False(routeInfo.ModelState.IsValid);
        }

        private RouteContext GetRouteContext(string url, string method = "GET", string queryString = null,
            string body = null, string contentType = null)
        {
            var httpContext = new HttpContextMock();
            httpContext.Request.Path = new PathString(url);
            httpContext.Request.QueryString = new QueryString(queryString);
            httpContext.Request.Method = method;
            httpContext.Request.ContentType = contentType;

            if (body != null)
            {
                httpContext.Request.Body = new MemoryStream();
                var streamWriter = new StreamWriter(httpContext.Request.Body);
                streamWriter.Write(body);
                streamWriter.Flush();
                httpContext.Request.Body.Position = 0;
            }

            return new RouteContext(httpContext);
        }
    }
}
