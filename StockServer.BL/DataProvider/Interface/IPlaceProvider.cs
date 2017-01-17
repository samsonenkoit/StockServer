using StockServer.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.DataProvider.Interface
{
    public interface IPlaceProvider
    {
        Task CreateAsync(string userId, Place place);
        Task<IList<Place>> GetForUserAsync(string userId);
        Task<IList<PlaceInfo>> GetShortPlaceInAreaAsync(Geolocation geolocation, double radiusMetres, int limit);
        Task<Place> GetAsync(int id);
        //Task<IList<PlaceInfo>> GetShortPlaceForUserAsync(string userId);
    }
}
