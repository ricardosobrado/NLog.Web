﻿#if !ASP_NET_CORE
using System.Web;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Web.SessionState;
#else
using HttpContextBase = Microsoft.AspNetCore.Http.HttpContext;
#endif
using NLog.Web.LayoutRenderers;
using NSubstitute;
using Xunit;

namespace NLog.Web.Tests.LayoutRenderers
{
    public class AspNetSessionIDLayoutRendererTests : TestBase
    {
        [Fact]
        public void NullHttpContextRendersEmptyString()
        {
            var renderer = new AspNetSessionIdLayoutRenderer();

            string result = renderer.Render(new LogEventInfo());

            Assert.Empty(result);
        }

        [Fact]
        public void NullSessionRendersEmptyString()
        {
            var httpContext = Substitute.For<HttpContextBase>();
#if ASP_NET_CORE
            httpContext.Session.Returns(null as Microsoft.AspNetCore.Http.ISession);
#else
            httpContext.Session.Returns(null as HttpSessionStateWrapper);
#endif
            var renderer = new AspNetSessionIdLayoutRenderer();
            renderer.HttpContextAccessor = new FakeHttpContextAccessor(httpContext);

            string result = renderer.Render(new LogEventInfo());

            Assert.Empty(result);
        }

        [Fact]
        public void AvailableSessionRendersSessionId()
        {
            var expectedResult = "value";
            var httpContext = Substitute.For<HttpContextBase>();
#if ASP_NET_CORE
            httpContext.Session.Id.Returns(expectedResult);
#else
            httpContext.Session.SessionID.Returns(expectedResult);
#endif
            var renderer = new AspNetSessionIdLayoutRenderer();
            renderer.HttpContextAccessor = new FakeHttpContextAccessor(httpContext);

            string result = renderer.Render(new LogEventInfo());

            Assert.Equal(expectedResult, result);
        }
    }
}