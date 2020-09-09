using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class Publisher : User
    {
        public ICollection<Job> PostedJobs { get; set; }
    }
}
