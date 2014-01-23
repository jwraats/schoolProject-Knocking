using knocking_doors.Controller;
using knocking_doors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace knocking_doors
{
    public class KnockingDoors
    {
        private MapController mc;
        private Frame rootFrame;
        public Player Player { get; set; }
        public BitmapImage ImageStreet;
        public enum Difficulties { First_Time, Been_Here, Born_Here };
        public Difficulties Difficult { get; set; }

        public KnockingDoors()
        {
            mc = new MapController();
            Player = new Player();
        }

        public KnockingDoors(Frame rootFrame) : this()
        {
            this.rootFrame = rootFrame;
            this.changePage(typeof(MainPage));
        }

        public void changePage(Type page)
        {
            this.rootFrame.Navigate(page, this);
        }

        public void changeDoor()
        {
            if (Player.Level != null)
            {
                int radius = 0;
                int timeGiven = 10;
                if(this.Difficult.Equals(KnockingDoors.Difficulties.First_Time)){
                    radius = 50;
                    timeGiven = 100;
                }
                else if (this.Difficult.Equals(KnockingDoors.Difficulties.Been_Here))
                {
                    radius = 250;
                    timeGiven = 150;
                }
                else if (this.Difficult.Equals(KnockingDoors.Difficulties.Born_Here))
                {
                    radius = 500;
                    timeGiven = 250;
                }

                BasicGeoposition basGeo = mc.getRandomGeoposition(Player.Level.Longitude, Player.Level.Latitude, radius);
                Player.currentDoor = new Door() { Latitude = basGeo.Latitude, Longitude = basGeo.Longitude, TimeLeft = timeGiven, Address = mc.ReverseGeoLoc(basGeo.Latitude, basGeo.Longitude) };

            }

            if (Player.currentDoor != null)
            {
                this.ImageStreet = mc.getImageUrlFromGeoPoint(Player.currentDoor.Address);
            }
        }

        public void changePlayerLocation()
        {
            mc.updateLocation();
            if (mc.getGeoposition() != null)
            {
                Player.Latitude = mc.getGeoposition().Coordinate.Point.Position.Latitude;
                Player.Longitude = mc.getGeoposition().Coordinate.Point.Position.Longitude;
                this.ImageStreet = mc.getImageUrlFromGeoPoint(mc.ReverseGeoLoc(Player.Latitude, Player.Longitude));
            }
        }
    }
}
