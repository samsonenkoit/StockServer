using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class ShortPlaceInfo
    {
        public int Id { get; set; }
        public Geolocation GeoLocation { get; set; }
        public string Name { get; set; }

        public ShortPlaceInfo()
        {

        }

        public ShortPlaceInfo(int id, Geolocation geolocation, string name)
        {
            Id = id;
            GeoLocation = geolocation;
            Name = name;
        }
    }
}
