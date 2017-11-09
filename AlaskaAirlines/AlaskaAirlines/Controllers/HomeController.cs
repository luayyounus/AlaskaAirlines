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
            var emptySearchView = new SearchViewModel
            {
                FromAirport = null,
                ToAirport = null,
                Flights = null
            };
            return View(emptySearchView);
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            List<Airport> listOfAirports = Csv.GetAllAirports();

            var Airport = (from A in listOfAirports where A.Name.ToLower().Contains(Prefix.ToLower()) select new { A.Name });

            return Json(Airport, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Search(SearchViewModel searchViewModel)
        {
            var fromCode = Csv.GetCodeForName(searchViewModel.FromAirport);
            var toCode = Csv.GetCodeForName(searchViewModel.ToAirport);

            ViewBag.fromCode = fromCode;
            ViewBag.toCode = toCode;

            List<Flight> flights = Csv.SearchFlights(fromCode, toCode);

            var viewModel = new SearchViewModel
            {
                FromAirport = fromCode,
                ToAirport = toCode,
                Flights = flights
            };
            return PartialView("_Flights", viewModel);
        }

        [HttpPost]
        public ActionResult SortTable(string fromAirport, string toAirport, string sortBy)
        {
            List<Flight> flights = Csv.SearchFlights(fromAirport, toAirport);
            List<Flight> sortedFlights = new List<Flight>();

            switch (sortBy)
            {
                case "Flight":
                    sortedFlights = flights.OrderBy(flight => flight.FlightNumber).ToList();
                    break;
                case "Departure":
                    sortedFlights = flights.OrderBy(flightTime => flightTime.Departs.TimeOfDay).ToList();
                    break;
                case "Price":
                    sortedFlights = flights.OrderBy(flightPrice => flightPrice.MainCabinPrice).ToList();
                    break;
                default:
                    break;
            }

            var viewModel = new SearchViewModel
            {
                FromAirport = fromAirport,
                ToAirport = toAirport,
                Flights = sortedFlights
            };
            return PartialView("_FlightsTable", viewModel);
        }
    }
}