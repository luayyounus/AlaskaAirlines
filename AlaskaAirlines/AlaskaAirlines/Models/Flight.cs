using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlaskaAirlines.Models
{
    public class Flight
    {
        public string From { get; set; }
        public string To { get; set; }
        public int FlightNumber { get; set; }
        public DateTime Departs { get; set; }
        public DateTime Arrives { get; set; }
        public int MainCabinPrice { get; set; }
        public int FirstClassPrice { get; set; }
    }
}