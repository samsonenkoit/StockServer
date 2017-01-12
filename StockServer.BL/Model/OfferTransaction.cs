using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class OfferTransaction
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public int OfferId { get; set; }
        public string BuyUserId { get; set; }
        public int Amount { get; set; }
        public int? PointTransactionId { get; set; }
        public OfferTransactionType Type { get; set; }
    }
}
