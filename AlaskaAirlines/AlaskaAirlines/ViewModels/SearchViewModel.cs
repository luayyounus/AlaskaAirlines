using AlaskaAirlines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlaskaAirlines.ViewModels
{
    public class SearchViewModel
    {
        public string FromAirport { get; set; }
        public string ToAirport { get; set; }
        public List<Flight> Flights { get; set; }
    }
}