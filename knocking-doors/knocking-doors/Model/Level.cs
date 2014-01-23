using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knocking_doors.Model
{
    public class Level
    {
        public string Name { set; get; }
        public double GeoRange { set; get; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
