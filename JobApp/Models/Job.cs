using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class Job
    {
        public int ID { get; set; }

        [JsonIgnore]
        [Required]
        public Publisher Publisher { get; set; }

        [Required]
        public int PublisherId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Last Edited")]
        public DateTime LastEdited { get; set; }

        [JsonIgnore]
        [Display(Name = "Skills")]
        public ICollection<JobSkill> JobSkills { get; set; }

        [JsonIgnore]
        [Display(Name = "Cities")]
        public ICollection<CityJob> JobCities { get; set; }

        [JsonIgnore]
        public ICollection<SeekerJob> JobSeekers { get; set; }

        public double Lon { get; set; }

        public double Lat { get; set; }
    }
}
