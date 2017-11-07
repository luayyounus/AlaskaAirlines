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
            List<Flight> flights = new List<Flight>();
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
            List<Airport> listOfAirports = Csv.GetAllAirports();

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

            List<Airport> listOfAirports = Csv.GetAllAirports();

            //Matching the airport's code with CSV
            var fromCode = "";
            var toCode = "";

            foreach (Airport airport in listOfAirports)
            {
                if (airport.Name == searchViewModel.FromAirport || airport.Code == searchViewModel.FromAirport)
                {
                    fromCode = airport.Code;
                }
                if (airport.Name == searchViewModel.ToAirport || airport.Code == searchViewModel.ToAirport)
                {
                    toCode = airport.Code;
                }
            }

            return RedirectToAction("Availability", "Home", new { fromAirport = fromCode, toAirport = toCode });
        }


        [Route("home/availability/{fromAirport:maxlength(3):minlength(3)}/{toAirport:maxlength(3):minlength(3)}/")]
        [HttpGet]
        public ActionResult Availability(string fromAirport, string toAirport)
        {

            List<Flight> flights = Csv.SearchFlights(fromAirport, toAirport);

            var viewModel = new SearchViewModel
            {
                FromAirport = fromAirport,
                ToAirport = toAirport,
                Flights = flights
            };

            return View("index", viewModel);
        }
    }
}