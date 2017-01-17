using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class Offer: OfferInfo
    {
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int AvailableAmount { get; set; }
    }
}
