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
            return View();
        }

        //Form Autocomplete Feature
        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            List<Airport> listOfAirports = Cvs.GetAllAirports();

            var Airport = (from A in listOfAirports where A.Name.ToLower().Contains(Prefix.ToLower()) select new { A.Name });

            return Json(Airport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search()
        {
            return View();
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

                return View("Index", sameViewModel);
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
    }
}