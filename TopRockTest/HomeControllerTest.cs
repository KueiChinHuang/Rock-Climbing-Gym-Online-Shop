using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TopRock.Controllers;

namespace TopRockTest
{
    [TestClass]
    public class HomeControllerTest
    {
        HomeController homeController;

        [TestInitialize]
        public void TestInitialize()
        {
            homeController = new HomeController();
        }

        [TestMethod]
        public void IndexReturnSomething()
        {            
            var result = homeController.Index();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexLoadCorrectView()
        {
            var result = homeController.Index();
            var viewResult = (ViewResult)result;
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void PrivacyLoadCorrectView()
        {
            var result = homeController.Privacy();
            var viewResult = (ViewResult)result;
            Assert.AreEqual("Privacy", viewResult.ViewName);
        }
    }
}
