using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class SeekerSkill
    {
        public int SeekerID { get; set; }
        public Seeker Seeker { get; set; }

        public string SkillName { get; set; }
        public Skill Skill { get; set; }
    }
}
