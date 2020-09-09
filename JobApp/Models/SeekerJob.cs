using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class SeekerJob
    {
        public int SeekerID { get; set; }
        public Seeker Seeker { get; set; }

        public int JobID { get; set; }
        public Job Job { get; set; }
    }
}
