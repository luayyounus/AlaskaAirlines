using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AlaskaAirlines;
using AlaskaAirlines.Controllers;
using AlaskaAirlines.ViewModels;
using AlaskaAirlines.Models;

namespace AlaskaAirlinesTests
{

    [TestClass]
    public class WhenRequestingIndexPage
    {
        [TestMethod]
        public void ThenReturnTheHomeView()
        {
            //Arrange
            HomeController homeController = new HomeController();

            //Act
            ViewResult viewResult = homeController.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(viewResult);
        }
    }
   
    [TestClass]
    public class WhenSubmittingForm
    {

        [TestMethod]
        public void ThenReturnSameViewIfEmpty()
        {
            //Arrange
            SearchViewModel emptySearchViewModel = new SearchViewModel
            {
                FromAirport = "",
                ToAirport = "",
                Flights = null
            };
            
            //Act
            HomeController homeController = new HomeController();
            homeController.ModelState.AddModelError("formIncomplete", "formIncomplete");
            ViewResult result = homeController.Index(emptySearchViewModel) as ViewResult;

            //Assert
            Assert.IsTrue(result.ViewData.ModelState.Count > 0);
        }
    }
}
