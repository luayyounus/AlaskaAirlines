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
using Moq;

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
        public void ThenReturnSameViewIfEmptyForm()
        {
            //Arrange
            SearchViewModel emptySearchViewModel = new SearchViewModel
            {
                FromAirport = "",
                ToAirport = "",
                Flights = null
            };
            HomeController homeController = new HomeController();

            //Act
            homeController.ModelState.AddModelError("formIncomplete", "formIncomplete");
            ViewResult result = homeController.Index(emptySearchViewModel) as ViewResult;

            //Assert
            Assert.IsTrue(result.ViewData.ModelState.Count > 0);
        }
    }

    [TestClass]
    public class WhenSortingFlightsRequested
    {
        [TestMethod]
        public void ThenReturnSortedByDepartureTime()
        {
            //Arrange
            Csv csv = new Csv();
            DateTime today = DateTime.Today;
            List<Flight> unsortedFlights = new List<Flight>
            {
                new Flight
                {
                    From = "SEA", To = "LAX", Departs = new DateTime(today.Year, today.Month, today.Day, 9, 0 , 0) , Arrives = new DateTime(today.Year, today.Month, today.Day, 7, 0 , 0), MainCabinPrice = 100, FirstClassPrice = 200
                },
                new Flight
                {
                    From = "SEA", To = "LAX", Departs = new DateTime(today.Year, today.Month, today.Day, 4, 0 , 0) , Arrives = new DateTime(today.Year, today.Month, today.Day, 2, 0 , 0), MainCabinPrice = 110, FirstClassPrice = 220
                },
                new Flight
                {
                    From = "SEA", To = "LAX", Departs = new DateTime(today.Year, today.Month, today.Day, 3, 0 , 0) , Arrives = new DateTime(today.Year, today.Month, today.Day, 1, 0 , 0), MainCabinPrice = 104, FirstClassPrice = 210
                }
            };

            //Act
            List<Flight> sortedFlights = csv.SortFlights(unsortedFlights, "Departure");

            //Assert
            Assert.AreEqual(new DateTime(today.Year, today.Month, today.Day, 3, 0, 0), sortedFlights[0].Departs);
        }
    }
}
