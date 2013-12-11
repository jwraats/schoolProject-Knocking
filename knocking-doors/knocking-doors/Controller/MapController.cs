using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Maps;
using Windows.Devices.Geolocation;


namespace knocking_doors.Controller
{
    class MapController
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
            this.getLocation(); //One time get Location.
        }

        public async void getLocation()
        {
            try
            {
                pos = await geolocator.GetGeopositionAsync();

            }
            catch { }
        }

    }
}
