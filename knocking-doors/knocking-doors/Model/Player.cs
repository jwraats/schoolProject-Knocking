using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knocking_doors.Model
{
    public class Player
    {
        public string Name { get; set; }
        public Level Level { get; set; }
        public string CityName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Player()
        {
            Level = new Level();
        }
    }
}
