﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Windows.Data.Xml.Dom;
using System.Xml.Linq;



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

        public BitmapImage getImageUrlFromGeoPoint(string address)
        {

            string url = "https://cdn1.iconfinder.com/data/icons/musthave/128/Help.png";
            if (address != "" && address != null && address != " ")
            {
                url = "http://maps.googleapis.com/maps/api/streetview?size=640x640&location=" + address + "&sensor=false";
            }
            return new BitmapImage(new Uri(url, UriKind.Absolute));
        }


        public BitmapImage getImageUrlFromGeoPoint(double lat, double lon)
        {

            string url = "https://cdn1.iconfinder.com/data/icons/musthave/128/Help.png";
            if (lat != -90000 && lon != -90000)
            {
                url = "http://maps.googleapis.com/maps/api/streetview?size=640x640&location=" + lat + "," + lon + "&sensor=false";
            }
            return new BitmapImage(new Uri(url, UriKind.Absolute));
        }

        public BitmapImage getImageUrlFromGeoPoint(Geopoint point)
        {
            if (point != null)
            {
                return this.getImageUrlFromGeoPoint(point.Position.Latitude, point.Position.Longitude);
            }
            else
            {
                return this.getImageUrlFromGeoPoint(-90000, -90000);
            }
            
        }

        public string getCityName()
        {
            return "Onbekend";
        }


        public BasicGeoposition getRandomGeoposition(double longitude, double latitude, int radius)
        {
            Random r = new Random();
            BasicGeoposition newLocation = new BasicGeoposition();
            

            double radiusInDegrees = radius / 111000f;

            double u, v, w, t, x, y, new_x, randomLong, randomLat;

            u = r.NextDouble();
            v = r.NextDouble();
            w = radiusInDegrees * Math.Sqrt(u);
            t = 2 * Math.PI * v;
            x = w * Math.Cos(t);
            y = w * Math.Sin(t);

            new_x = x / Math.Cos(longitude);
            randomLong = new_x + longitude;
            randomLat = y + latitude;

            newLocation.Longitude = randomLong;
            newLocation.Latitude = randomLat;

            return newLocation;

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


        public string ReverseGeoLoc(double latitude, double longitude)
        {
            try
            {

                System.Diagnostics.Debug.WriteLine("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false");
                using (XmlReader reader = XmlReader.Create("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false")) {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (reader.Name == "result")
                                {
                                    XElement el = XElement.ReadFrom(reader) as XElement;
                                    if (el != null)
                                    {
                                        return el.Element("formatted_address").Value;
                                    }
                                }
                                break;
                        }
                    } 


                    return reader.ReadElementContentAsString();
                }
                return "Oeepppsss...";
                

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine( ex.Message);
                return "Jammmeeerrrr";
            }
        }

    }
}
