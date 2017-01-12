using StockServer.BL.DataProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockServer.BL.Model;
using AutoMapper;
using System.Diagnostics;
using System.Data.Entity;

namespace StockServer.DL.DataProvider
{
    public class OfferProvider : IOfferProvider
    {
        private readonly StockDbEntities _dbContext;
        private readonly IMapper _mapper;

        public OfferProvider(StockDbEntities dbContext, IMapper mapper)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            _dbContext = dbContext;
            _mapper = mapper;

#if DEBUG
            _dbContext.Database.Log = (a) => Debug.Write(a);
#endif
        }

        public async Task AddTransactionAsync(OfferTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            DL.OfferTransactions dbTr = _mapper.Map<DL.OfferTransactions>(transaction);
            dbTr.CreateDate = DateTime.UtcNow;

            _dbContext.OfferTransactions.Add(dbTr);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(BL.Model.Offer offer)
        {
            if (offer == null) throw new ArgumentNullException(nameof(offer));

            DL.Offer dbOffer = _mapper.Map<DL.Offer>(offer);

            _dbContext.Offer.Add(dbOffer);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IList<BL.Model.Offer>> GetAllAsync(string userId, int placeId)
        {
            var dbOffers = await (from user in _dbContext.AspNetUsers
                            from place in user.Place
                            from offer in place.Offer
                            where user.Id == userId && place.Id == placeId
                            select new
                            {
                                Id = offer.Id,
                                Title = offer.Title,
                                Description = offer.Description,
                                Price = offer.Price,
                                IsActive = offer.IsActive,
                                PlaceId = offer.PlaceId,
                                AvailableAmount = offer.OfferTransactions.Select(t => t.Amount).DefaultIfEmpty(0).Sum()
                            }).ToListAsync();

            var offers = dbOffers.Select(t => new BL.Model.Offer()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Price = t.Price,
                IsActive = t.IsActive,
                PlaceId = t.PlaceId,
                AvailableAmount = t.AvailableAmount
            });
            
            return offers.ToList();
        }

        public async Task<IList<Purchase>> GetPlacePurchase(string userId, int placeId)
        {
            var offersInf = await (from user in _dbContext.AspNetUsers
                            join offerTr in _dbContext.OfferTransactions on user.Id equals offerTr.BuyUserId
                            join delivery in _dbContext.UserOfferDelivery on offerTr.Id equals delivery.OfferTransactionId
                            join offer in _dbContext.Offer on offerTr.OfferId equals offer.Id
                            where delivery == null && offerTr.TypeId == (int)BL.Model.OfferTransactionType.Buy
                            select new
                            {
                                UserId = user.Id,
                                UserName = user.UserName,
                                OfferId = offer.Id,
                                PlaceId = offer.PlaceId,
                                OfferTitle = offer.Title,
                                TransactionId = offerTr.Id,
                                Amount = offerTr.Amount,
                                TrType = offerTr.TypeId
                            }).ToListAsync();

            var purshase = offersInf.Select(t => new Purchase()
            {
                User = new UserInfo()
                {
                    Id = t.UserId,
                    Name = t.UserName
                },
                Offer = new OfferInfo()
                {
                    Id = t.OfferId,
                    PlaceId = t.PlaceId,
                    Title = t.OfferTitle
                },
                Transaction = new OfferTransaction()
                {
                    Id = t.TransactionId,
                    OfferId = t.OfferId,
                    Amount = t.Amount,
                    Type = (BL.Model.OfferTransactionType)t.TrType
                }
            }).ToList();

            return purshase;
        }
    }
}
