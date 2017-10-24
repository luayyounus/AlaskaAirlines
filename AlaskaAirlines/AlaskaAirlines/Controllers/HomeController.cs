using AlaskaAirlines.Models;
using AlaskaAirlines.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlaskaAirlines.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Flight> flights = Cvs.SearchFlights("", "");
            var viewModel = new SearchViewModel
            {
                FromAirport = "",
                ToAirport = "",
                Flights = flights
            };
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            List<Airport> listOfAirports = Cvs.GetAllAirports();

            var Airport = (from A in listOfAirports where A.Name.ToLower().Contains(Prefix.ToLower()) select new { A.Name });

            return Json(Airport, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel searchViewModel)
        {

            //Form Validation
            if (!ModelState.IsValid)
            {
                var sameViewModel = new SearchViewModel
                {
                    FromAirport = searchViewModel.FromAirport,
                    ToAirport = searchViewModel.ToAirport,
                    Flights = new List<Flight>()
                };

                return View("Index", sameViewModel);
            }

            List<Airport> listOfAirports = Cvs.GetAllAirports();

            //Matching the airport's code with CSV
            var fromCode = "";
            var toCode = "";

            foreach (Airport airport in listOfAirports)
            {
                if (airport.Name == searchViewModel.FromAirport)
                {
                    fromCode = airport.Code;
                }
                if (airport.Name == searchViewModel.ToAirport)
                {
                    toCode = airport.Code;
                }
            }

            return RedirectToAction("Availability", "Home", new { fromAirport = fromCode, toAirport = toCode });
        }


        [Route("home/availability/{fromAirport:maxlength(3):minlength(3)}/{toAirport:maxlength(3):minlength(3)}/{sortBy?}")]
        [HttpGet]
        public ActionResult Availability(string fromAirport, string toAirport, string sortBy)
        {

            ViewBag.SortByFlight = string.IsNullOrWhiteSpace(sortBy) ? "Flight" : "";
            ViewBag.SortByDeparture = "Departure";
            ViewBag.SortByPrice = "Price";

            List<Flight> flights = Cvs.SearchFlights(fromAirport, toAirport);
            List<Flight> sortedFlights = new List<Flight>();

            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy == "Flight")
                {
                    sortedFlights = flights.OrderBy(flight => flight.FlightNumber).ToList();
                }
                else if (sortBy == "Departure")
                {
                    sortedFlights = flights.OrderBy(flightTime => flightTime.Departs.TimeOfDay).ToList();
                }
                else if (sortBy == "Price")
                {
                    sortedFlights = flights.OrderBy(flightPrice => flightPrice.MainCabinPrice).ToList();
                }
            }
            else
            {
                sortedFlights = flights;
            }

            var viewModel = new SearchViewModel
            {
                FromAirport = fromAirport,
                ToAirport = toAirport,
                Flights = sortedFlights
            };
            return View("index", viewModel);
        }
    }
}