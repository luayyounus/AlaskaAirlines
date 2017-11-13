using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AlaskaAirlines;
using AlaskaAirlines.Controllers;

namespace AlaskaAirlinesTests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
            HomeController homeController = new HomeController();
            
            //Act
            ViewResult viewResult = homeController.Index() as ViewResult;
            
            //Assert
            Assert.IsNotNull(viewResult);
        }
    }
}
