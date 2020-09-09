using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class CityJob
    {
        public string CityName { get; set; }
        public City City { get; set; }

        public int JobID { get; set; }
        public Job Job { get; set; }
    }
}
