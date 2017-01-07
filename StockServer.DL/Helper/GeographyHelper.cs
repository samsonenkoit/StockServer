using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.DL.Helper
{
    public static class GeographyHelper
    {
        private const int DEFAULT_COORDINATE_SYSTEM = 4326;

        public static DbGeography PointFromGeoPoint(StockServer.BL.Model.Geolocation geoPoint)
        {
            var nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            string text = $"POINT({geoPoint.Longitude.ToString(nfi)} {geoPoint.Latitude.ToString(nfi)})";

            return DbGeography.PointFromText(text, DEFAULT_COORDINATE_SYSTEM);
        }

       /* public static double DistanceBetween(DbGeography geoPoint1, GeoPoint geoPoint2)
        {
            if (!geoPoint1.Latitude.HasValue || !geoPoint1.Longitude.HasValue)
            {
                throw new Exception("geoPoint1 не содержит всех необходимых данных");
            }

            var geoPoint = new GeoPoint
            {
                Latitude = geoPoint1.Latitude.Value,
                Longitude = geoPoint1.Longitude.Value
            };

            return DistanceBetween(geoPoint, geoPoint2);
        }*/

     /*   public static double DistanceBetween(GeoPoint geoPoint1, GeoPoint geoPoint2)
        {
            double rlat1 = Math.PI * geoPoint1.Latitude / 180;
            double rlat2 = Math.PI * geoPoint2.Latitude / 180;
            double theta = geoPoint1.Longitude - geoPoint2.Longitude;
            double rtheta = Math.PI * theta / 180;
            double distanceBetween = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            distanceBetween = Math.Acos(distanceBetween);
            distanceBetween = distanceBetween * 180 / Math.PI;
            distanceBetween = distanceBetween * 60 * 1.852; //количество километров в морской миле

            return distanceBetween;
        }*/
    }
}
