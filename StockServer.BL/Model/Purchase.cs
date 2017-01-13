using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class Purchase
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int OfferId { get; set; }
        public string OfferTitle { get; set; }
        public int OfferTransactionId { get; set; }
        //public int PointTransactionId { get; set; }
        public int Amount { get; set; }
        public int PlaceId { get; set; }
    }
}
