using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public class SeekerRegion
    {
        public int SeekerID { get; set; }
        public Seeker Seeker { get; set; }

        public string RegionName { get; set; }
        public Region Region { get; set; }
    }

    public class SeekerRegionCheckBoxItem
    {
        public string RegionName { get; set; }

        public bool IsChecked { get; set; }
    }

    public class SeekerRegionViewModel
    {
        public List<SeekerRegionCheckBoxItem> RegionCheckBoxItems { get; set; }
    }
}
