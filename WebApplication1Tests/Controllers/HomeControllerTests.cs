using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using RazorGenerator.Testing;

namespace WebApplication1.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private HomeController controller = new HomeController();

        private Mock<ControllerContext> controllerContext = new Mock<ControllerContext>();
        private Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
        private Mock<HttpRequestBase> requestContext = new Mock<HttpRequestBase>();
        private Mock<HttpResponseBase> responseContext = new Mock<HttpResponseBase>();
        private Mock<HttpBrowserCapabilitiesBase> browser = new Mock<HttpBrowserCapabilitiesBase>();
        private Mock<HttpSessionStateBase> session = new Mock<HttpSessionStateBase>();
        private Mock<HttpServerUtilityBase> server = new Mock<HttpServerUtilityBase>();
        private Mock<HttpCachePolicyBase> cache = new Mock<HttpCachePolicyBase>();
        private RouteData route= new RouteData();
        private object viewName { get { return route.Values["action"]; } set { this.route.Values["action"] = value; } }

        [TestInitialize]
        public void Init()
        {
            this.controller.ControllerContext = this.controllerContext.Object;
            this.controllerContext.Setup(mock => mock.HttpContext).Returns(this.httpContext.Object);
            this.httpContext.Setup(mock => mock.Request).Returns(this.requestContext.Object);
            this.httpContext.Setup(mock => mock.Response).Returns(this.responseContext.Object);
            this.controllerContext.Setup(mock => mock.RouteData).Returns(this.route); 
            this.requestContext.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns("~/");
            var controllerName = this.controller.GetType().Name;
            this.route.Values["controller"] = controllerName;
            var actionResultOutput = new StringWriter();
            this.responseContext.Setup(mock => mock.Output).Returns(actionResultOutput);
            this.httpContext.Setup(mock => mock.Items).Returns(new Dictionary<object,object>() );
            var cookieCollection = new HttpCookieCollection { };
            this.requestContext.Setup(mock => mock.Cookies).Returns(cookieCollection);
            this.responseContext.Setup(mock => mock.Cookies).Returns(cookieCollection);
            this.requestContext.Setup(mock => mock.ValidateInput());
            this.requestContext.Setup(mock => mock.UserAgent).Returns("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11");
            this.requestContext.Setup(mock => mock.Browser).Returns(browser.Object);
            this.httpContext.SetupGet(mock => mock.Session).Returns(session.Object);
            this.httpContext.SetupGet(mock => mock.Server).Returns(server.Object);
        }

        [TestMethod()]
        public void IndexTest()
        {
            this.viewName = "Index";
            var actual = this.controller.Index();
            actual.ExecuteResult(this.controller.ControllerContext);
        }
    }
}