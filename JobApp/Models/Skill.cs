using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class Skill
    {
        [Key]
        [Display(Name = "Skill")]
        public string Name { get; set; }

        public ICollection<SeekerSkill> SkillSeekers { get; set; }

        public ICollection<JobSkill> SkillJobs { get; set; }
    }
}
