using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.OfferViewModels
{
    public class AddOfferItemsViewModel
    {
        public int OfferId { get; set; }
        public int PlaceId { get; set; }

        [Range(1, int.MaxValue)]
        public int Amount { get; set; } = 1;
    }
}
