using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;


namespace knocking_doors.Controller
{
    public class MapController
    {
        private Geolocator geolocator;
        private Geoposition pos;
        private Map bingMap;
        
        //Bingmaps API:
        //Aq5iV1oO3-YjhxwTyGv0W6_hATShyfuhYI2LaJqak7a4UjTfC9zMv5YLTW7eMiFc


        public MapController()
        {
            //Creating the Bing maps
            bingMap = new Map();
            bingMap.Credentials = "Aq5iV1oO3-YjhxwTyGv0W6_hATShyfuhYI2LaJqak7a4UjTfC9zMv5YLTW7eMiFc";


            //Set our own GeoLocator
            geolocator = new Geolocator();
            geolocator.DesiredAccuracy = Windows.Devices.Geolocation.PositionAccuracy.High;
            
            //For get location;
            pos = null;
            this.updateLocation(); //One time get Location.
        }

        public BitmapImage getImageUrlFromGeoPoint(Geopoint point)
        {
            string url = "https://cdn1.iconfinder.com/data/icons/musthave/128/Help.png";
            if(point != null){
                url = "http://maps.googleapis.com/maps/api/streetview?size=640x640&location=" + point.Position.Latitude + "," + point.Position.Longitude + "&sensor=false";
            }
            return new BitmapImage(new Uri(url, UriKind.Absolute));
        }

        public string getCityName()
        {
            
            return "Onbekend";
        }

        public Geoposition getGeoposition()
        {
            return this.pos;
        }

        public async void updateLocation()
        {
            try
            {
                this.pos = await geolocator.GetGeopositionAsync();

            }
            catch { }
        }

    }
}
