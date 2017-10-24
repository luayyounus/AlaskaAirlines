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

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            List<Airport> listOfAirports = Cvs.GetAllAirports();

            var Airport = (from A in listOfAirports where A.Name.ToLower().Contains(Prefix.ToLower()) select new { A.Name });

            return Json(Airport, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Search(AirportsViewModel airportsViewModel)
        {
            //Form Validation
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

            //Matching the airport's code with CSV
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