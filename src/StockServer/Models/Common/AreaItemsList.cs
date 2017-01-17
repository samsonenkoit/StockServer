using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.Common
{
    public class AreaItemsList<T>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }

        public List<T> Items { get; set; }
    }
}
