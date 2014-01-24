using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knocking_doors.Model
{
    public class Player : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public Level Level { get; set; }
        public string CityName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Door currentDoor { get; set; }
        public int Score { get; set; }
        public int ScoreBind
        {
            get { return Score; }
            set
            {
                Score = value;
                NotifyPropertyChanged("Score");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public Player()
        {
            Level = new Level();
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
