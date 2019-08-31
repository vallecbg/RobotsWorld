using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RobotsWorld.Services.Constants;

namespace RobotsWorld.Web.HelperMethods
{
    public static class CalculationMethods
    {
        public static double[] GetCoordinates(string locationName)
        {
            var locationService = new GoogleLocationService(GlobalConstants.GoogleMapsApiKey);
            var point = locationService.GetLatLongFromAddress(locationName);

            var latitude = point.Latitude;
            var longitude = point.Longitude;

            var result = new double[]
            {
                latitude,
                longitude
            };

            return result;
        }
    }
}
