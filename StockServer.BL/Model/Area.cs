using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class Area: Geolocation
    {
        public double Radius { get; set; }
    }
}
