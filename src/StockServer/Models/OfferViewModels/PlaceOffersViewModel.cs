using StockServer.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.OfferViewModels
{
    public class PlaceOffersViewModel
    {
        public int PlaceId { get; set; }

        public IEnumerable<OfferViewModel> Offers { get; set; }
    }
}
