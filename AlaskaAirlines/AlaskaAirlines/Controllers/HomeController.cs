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
            Csv csv = new Csv();
            List<Airport> listOfAirports = csv.GetAllAirports();

            var Airport = (from A in listOfAirports where A.Name.ToLower().Contains(Prefix.ToLower()) select new { A.Name });

            return Json(Airport, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SearchViewModel searchViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(searchViewModel);
            }
            Csv csv = new Csv();
            var fromCode = csv.GetCodeForName(searchViewModel.FromAirport);
            var toCode = csv.GetCodeForName(searchViewModel.ToAirport);

            return RedirectToAction("Search", "Flight", new { fromAirport = fromCode, toAirport = toCode });
        }
    }
}