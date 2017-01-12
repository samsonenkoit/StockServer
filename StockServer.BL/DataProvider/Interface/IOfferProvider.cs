using StockServer.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.DataProvider.Interface
{
    public interface IOfferProvider
    {
        Task<IList<Offer>> GetAllAsync(string userId, int placeId);
        Task<IList<Purchase>> GetPlacePurchase(string userId, int placeId);
        Task CreateAsync(Offer offer);
        Task AddTransactionAsync(OfferTransaction transaction);
    }
}
