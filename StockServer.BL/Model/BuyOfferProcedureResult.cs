using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public enum BuyOfferProcedureResult
    {
        Success = 1,
        NotEnoughPoints = 101,
        NotEnoughOffers = 102
    }
}
