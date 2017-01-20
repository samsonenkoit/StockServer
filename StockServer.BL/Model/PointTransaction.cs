using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class PointTransaction
    {
        public int Id { get; set; }
        public DateTime  CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public PointTransactionType Type { get; set; }
    }
}
