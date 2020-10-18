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
        [Display(Name = "City")]
        public string Name { get; set; }

        [Required]
        public Region Region { get; set; }

        [Required]
        public String RegionName { get; set; }

        public ICollection<CityJob> CityJobs { get; set; }
    }
}
