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
        Task<IList<Purchase>> GetPurchasesAsync(string offerOwnerUserId, string buyUserId, int? placeId, bool? onlyNotDelivered);
        Task<Purchase> GetPurchaseAsync(int offerTrId);
        Task CreateAsync(Offer offer);
        Task<IList<Offer>> GetOffersAsync(string ownerUserId, Area area, int? placeId, bool? isActive, int? minItemsAmount, int? limit);
        Task<Offer> GetAsync(int id);
        Task UpdateAsync(Offer offer);
        Task AddTransactionAsync(OfferTransaction transaction);
        Task DeliverPurchaseAsync(string createUserId, int offerTransactionId);
        Task<BuyOfferProcedureResult> BuyAsync(string userId, int offerId, int amount);

    }
}
