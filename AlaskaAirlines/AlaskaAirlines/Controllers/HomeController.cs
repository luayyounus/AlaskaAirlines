using AlaskaAirlines.Models;
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
    }
}