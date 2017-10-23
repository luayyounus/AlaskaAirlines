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
        [Route("flight/availability/{fromAirport:maxlength(3):minlength(3)}/{toAirport:maxlength(3):minlength(3)}/{sortBy?}")]
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
            return View(viewModel);
        }
    }
}