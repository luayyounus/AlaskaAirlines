using AlaskaAirlines.Models;
using AlaskaAirlines.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlaskaAirlines.Controllers
{
    public class FlightController : Controller
    {

        [HttpGet]
        public ActionResult Search(string fromAirport, string toAirport)
        {
            if(string.IsNullOrEmpty(fromAirport) || string.IsNullOrEmpty(toAirport))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.fromCode = fromAirport;
            ViewBag.toCode = toAirport;

            List<Flight> flights = Csv.SearchFlights(fromAirport, toAirport);

            var viewModel = new SearchViewModel
            {
                FromAirport = fromAirport,
                ToAirport = toAirport,
                Flights = flights
            };
            return View("Flights", viewModel);
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