using Microsoft.AspNetCore.Mvc.Rendering;
using StockServer.BL.Model;
using StockServer.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.OfferViewModels
{
    public class CreateEditOfferView
    {
        public OfferViewModel Offer { get; set; }
        public SelectList Places { get; set; }
    }
}
