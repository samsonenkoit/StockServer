using StockServer.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.Common
{
    public class PlaceInfoAggregate
    {
        public PlaceViewModel Place { get; set; }
        public List<OfferInfo> Offers { get; set; }
    }
}
