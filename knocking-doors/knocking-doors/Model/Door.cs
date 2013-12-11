using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knocking_doors.Model
{
    public class Door
    {
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int TimeLeft { get; set; }

    }
}
