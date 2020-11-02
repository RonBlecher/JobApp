﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class Region
    {
        [Key]
        [Display(Name = "Region Name")]
        [RegularExpression("^[A-Za-z '-]+$", ErrorMessage = "{0} is not valid.")]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        public ICollection<SeekerRegion> RegionSeekers { get; set; }
    }
}
