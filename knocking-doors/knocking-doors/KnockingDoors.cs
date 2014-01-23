using knocking_doors.Controller;
using knocking_doors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void changePlayerLocation()
        {
            mc.updateLocation();
            if (mc.getGeoposition() != null)
            {
                this.ImageStreet = mc.getImageUrlFromGeoPoint(mc.getGeoposition().Coordinate.Point);
                Player.Latitude = mc.getGeoposition().Coordinate.Point.Position.Latitude;
                Player.Longitude = mc.getGeoposition().Coordinate.Point.Position.Longitude;
                
            }
        }
    }
}
