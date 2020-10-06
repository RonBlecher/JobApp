using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class JobSkill
    {
        public int JobID { get; set; }
        public Job Job { get; set; }

        public string SkillName { get; set; }
        public Skill Skill { get; set; }
    }
}
