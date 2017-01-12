using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class Purchase
    {
        public UserInfo User { get; set; }
        public OfferInfo Offer { get; set; }
        public OfferTransaction Transaction { get; set; }
    }
}
