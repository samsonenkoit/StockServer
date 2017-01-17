using StockServer.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.Common
{
    public class UserInfoAggregate
    {
        public UserInfo User { get; set; }
        public List<PurchaseInfo> Purchases { get; set; }
    }
}
