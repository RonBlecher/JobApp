using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class Publisher : User
    {
        [JsonIgnore]
        public ICollection<Job> PostedJobs { get; set; }
    }
}
