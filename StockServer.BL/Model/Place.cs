﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.Model
{
    public class Place: PlaceInfo
    {
        public string Address { get; set; }
        public string Contact { get; set; }
        public string LogoUrl { get; set; }
        public string ViewUrl { get; set; }
        public string Description { get; set; }
    }
}
