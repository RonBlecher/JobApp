using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class JobViewModel
    {
        public int ID { get; set; }

        [Required]
        public int PublisherId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime LastEdited { get; set; }

        [Required]
        [Display(Name = "Skills")]
        public ICollection<string> JobSkillsId { get; set; }

        [Required]
        [Display(Name = "Cities")]
        public ICollection<string> JobCities { get; set; }

        public ICollection<SeekerJob> JobSeekers { get; set; }

        [Required]
        public double Lon { get; set; }

        [Required]
        public double Lat { get; set; }
    }
}
