using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class PurchaseInfo
    {
        public int OfferTransactionId { get; set; }

        public int Amount { get; set; }

        public OfferInfo Offer { get; set; }
    }
}
