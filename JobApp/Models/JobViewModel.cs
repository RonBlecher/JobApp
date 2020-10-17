using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class JobViewModel
    {
        [Required]
        public int PublisherId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<string> JobSkillsId { get; set; }

        public ICollection<CityJob> JobCities { get; set; }

        public ICollection<SeekerJob> JobSeekers { get; set; }

        public double Lon { get; set; }

        public double Lat { get; set; }
    }
}
