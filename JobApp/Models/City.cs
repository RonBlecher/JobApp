using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class City
    {
        [Key]
        [Required]
        [Display(Name = "City")]
        public string Name { get; set; }

        [Required]
        public Region Region { get; set; }

        [Required]
        public string RegionName { get; set; }

        public ICollection<CityJob> CityJobs { get; set; }
    }
}
