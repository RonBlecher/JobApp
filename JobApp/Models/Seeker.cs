using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class Seeker : User
    {
        public ICollection<SeekerSkill> SeekerSkills { get; set; }

        public ICollection<SeekerJob> SeekerJobs { get; set; }

        public byte[] CV { get; set; }

        [NotMapped]
        [Display(Name = "CV File")]
        public IFormFile CVObj { get; set; }
    }
}
