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
        [RegularExpression("^[A-Za-z '-]+$", ErrorMessage = "{0} is not valid.")]
        public string Name { get; set; }

        [Required]
        public Region Region { get; set; }

        [Required]
        [Display(Name = "Region")]
        public string RegionName { get; set; }

        public ICollection<CityJob> CityJobs { get; set; }
    }

    public class CityListView
    {
        [Display(Name = "City")]
        public string Name { get; set; }

        [Display(Name = "Region")]
        public string RegionName { get; set; }

        [Display(Name = "Jobs Number")]
        public int CityJobsNum { get; set; }
    }
}
