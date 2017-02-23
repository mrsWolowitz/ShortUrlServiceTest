using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortUrlServiceWeb;
using ShortUrlServiceWeb.Controllers;

namespace ShortUrlServiceWeb.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }

        [TestMethod]
        public void History()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.History() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("History", result.ViewBag.Title);
        }
    }
}
