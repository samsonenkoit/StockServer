using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.DL
{
    internal class Converter

    {
        internal static BL.Model.Place ToPlace(DL.Place place)
        {
            BL.Model.Place pl = new BL.Model.Place()
            {
                Id = place.Id,
                Name = place.Name,
                Address = place.Address,
                Contact = place.Contact,
                LogoUrl = place.LogoUrl,
                ViewUrl = place.ViewUrl,
            };

            return pl;
        }

      //  internal static BL.Model.Geolocation ToGeolocation()
    }
}
