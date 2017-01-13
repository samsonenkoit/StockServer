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
        Task<IList<Offer>> GetPlaceOffersAsync(string userId, int placeId);
        Task<IList<Purchase>> GetPurchaseAsync(string userId, int? placeId);
        Task CreateAsync(Offer offer);
        Task AddTransactionAsync(OfferTransaction transaction);
        Task<IList<Offer>> GetOffersInAreaAsync(Geolocation geolocation, double radiusMetres, int limit);
        Task<BuyOfferProcedureResult> BuyAsync(string userId, int offerId, int amount);
        //Task<OfferTransaction> GetUserPurchase(string useruId);

    }
}
