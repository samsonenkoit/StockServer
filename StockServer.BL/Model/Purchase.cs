﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class Purchase: PurchaseInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

    }
}
