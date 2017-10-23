﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlaskaAirlines.ViewModels
{
    public class AirportsViewModel
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Departure Airport")]
        public string FromAirport { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Arrival Airport")]
        public string ToAirport { get; set; }
    }
}