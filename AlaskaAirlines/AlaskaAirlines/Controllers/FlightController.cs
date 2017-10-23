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
        //Redirecting a get request to Index page
        [HttpGet]
        public ActionResult Search()
        {
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult Search(AirportsViewModel airportsViewModel)
        {
            //checking if the input form is Empty
            if (!ModelState.IsValid)
            {
                var sameViewModel = new AirportsViewModel
                {
                    FromAirport = airportsViewModel.FromAirport,
                    ToAirport = airportsViewModel.ToAirport,
                };
                return View("Index", airportsViewModel);
            }

            List<Airport> listOfAirports = Cvs.GetAllAirports();

            //Grabbing the Code for the Airports Names entered in the Form
            var fromCode = "";
            var toCode = "";

            foreach (Airport airport in listOfAirports)
            {
                if (airport.Name == airportsViewModel.FromAirport)
                {
                    fromCode = airport.Code;
                }
                if (airport.Name == airportsViewModel.ToAirport)
                {
                    toCode = airport.Code;
                }
            }

            return RedirectToAction("Availability", "Flight", new { fromAirport = fromCode, toAirport = toCode });
        }

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