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
        // https://stackoverflow.com/questions/7739233/double-escape-sequence-inside-a-url-the-request-filtering-module-is-configured
        [RegularExpression(@"^(?!.*[\/;?:@&=+$,]).+$", ErrorMessage = "{0} can't accept double escape sequences.")]
        public string Name { get; set; }

        public ICollection<SeekerSkill> SkillSeekers { get; set; }

        public ICollection<JobSkill> SkillJobs { get; set; }
    }
}
