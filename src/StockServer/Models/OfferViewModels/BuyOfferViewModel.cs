using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.OfferViewModels
{
    public class BuyOfferViewModel
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        [Range(1,100)]
        public int Amount { get; set; }
    }
}
