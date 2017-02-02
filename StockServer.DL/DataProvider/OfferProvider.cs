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
using StockServer.DL.Helper;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Data;

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

        public async Task<BuyOfferProcedureResult> BuyAsync(string userId, int offerId, int amount)
        {
            var nowUtc = DateTime.UtcNow;

            string commandStr = "exec @ReturnCode = dbo.BuyOfferProcedure @createUserId, @buyUserId,@offerId,@createDate,@amount";
            List<SqlParameter> cmdParams = new List<SqlParameter>();

            cmdParams.Add(new SqlParameter("@createUserId", userId)
            {
                DbType = System.Data.DbType.String
            });
            cmdParams.Add(new SqlParameter("@buyUserId", userId) {
                DbType = System.Data.DbType.String
            });
            cmdParams.Add(new SqlParameter("@offerId", offerId)
            {
                DbType = System.Data.DbType.Int32
            });
            cmdParams.Add(new SqlParameter("@createDate", DateTime.UtcNow)
            {
                DbType = System.Data.DbType.DateTime
            });
            cmdParams.Add(new SqlParameter("@amount", amount)
            {
                DbType = System.Data.DbType.Int32
            });

            var returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.SqlDbType = SqlDbType.Int;
            returnCode.Direction = ParameterDirection.Output;

            cmdParams.Add(returnCode);

            var result = await _dbContext.Database.ExecuteSqlCommandAsync(commandStr, cmdParams.ToArray());

            return (BuyOfferProcedureResult)(int)returnCode.Value;

                
        }

        public async Task CreateAsync(BL.Model.Offer offer)
        {
            if (offer == null) throw new ArgumentNullException(nameof(offer));

            DL.Offer dbOffer = _mapper.Map<DL.Offer>(offer);

            _dbContext.Offer.Add(dbOffer);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task DeliverPurchaseAsync(string createUserId, int offerTransactionId)
        {
            DL.UserOfferDelivery del = new UserOfferDelivery()
            {
                CreateDate = DateTime.UtcNow,
                CreateUserId = createUserId,
                OfferTransactionId = offerTransactionId
            };

            _dbContext.UserOfferDelivery.Add(del);
            return _dbContext.SaveChangesAsync();

        }

        public async Task<BL.Model.Offer> GetAsync(int id)
        {
            var offers = await GetOffersAsync(id, null, null, null, null, null, null);
            return offers.FirstOrDefault();
        }

        public Task<IList<BL.Model.Offer>> GetOffersAsync(string ownerUserId, Area area, int? placeId, bool? isActive, int? minItemsAmount, int? limit)
        {
            return GetOffersAsync(null, ownerUserId, area, placeId, isActive, minItemsAmount, limit);
        }

        private async Task<IList<BL.Model.Offer>> GetOffersAsync(int? offerId, string ownerUserId, Area area, int? placeId, bool? isActive, int? minItemsAmount, int? limit)
        {
            #region query
            var query = from user in _dbContext.AspNetUsers
                        from place in user.Place
                        from offer in place.Offer
                        select new
                        {
                            User = user,
                            Place = place,
                            Offer = offer,
                            OfferAmount = offer.OfferTransactions.Select(t => t.Amount).DefaultIfEmpty(0).Sum()
                        };

            if (area != null)
            {
                var radiusSm = area.RadiusMeters * 100;
                DbGeography dbArea = GeographyHelper.PointFromGeoPoint(area).Buffer(radiusSm);

                query = query.Where(t => SqlSpatialFunctions.Filter(t.Place.GeoPoint, dbArea) == true);
            }

            if (offerId != null)
                query = query.Where(t => t.Offer.Id == offerId.Value);

            if (!string.IsNullOrEmpty(ownerUserId))
                query = query.Where(t => t.User.Id == ownerUserId);

            if (placeId != null)
                query = query.Where(t => t.Place.Id == placeId.Value);

            if (isActive != null)
                query = query.Where(t => t.Offer.IsActive == isActive.Value);

            if (minItemsAmount != null)
                query = query.Where(t => t.OfferAmount >= minItemsAmount.Value);

            if (limit != null)
                query = query.Take(limit.Value);

            #endregion

            var dbOffers = await query.Select(t =>
            new
            {
                Id = t.Offer.Id,
                Title = t.Offer.Title,
                Description = t.Offer.Description,
                Price = t.Offer.Price,
                IsActive = t.Offer.IsActive,
                PlaceId = t.Offer.PlaceId,
                AvailableAmount = t.OfferAmount,
                LogoUrl = t.Offer.LogoUrl
                
            }).ToListAsync().ConfigureAwait(false);

            var offers = dbOffers.Select(t => new BL.Model.Offer()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Price = t.Price,
                IsActive = t.IsActive,
                PlaceId = t.PlaceId,
                AvailableAmount = t.AvailableAmount,
                LogoUrl = t.LogoUrl
            });

            return offers.ToList();
        }

        public async Task<Purchase> GetPurchaseAsync(int offerTrId)
        {
            var purchases = await GetPurchasesAsync(offerTrId, null, null, null, null).ConfigureAwait(false);

            return purchases.FirstOrDefault();
        }

        public Task<IList<Purchase>> GetPurchasesAsync(string offerOwnerUserId, string buyUserId, int? placeId, bool? onlyNotDelivered)
        {
            return GetPurchasesAsync(null, offerOwnerUserId, buyUserId, placeId, onlyNotDelivered);
        }

        private async Task<IList<Purchase>> GetPurchasesAsync(int? offerTrId, string offerOwnerUserId, string buyUserId, int? placeId, bool? onlyNotDelivered)
        {
            var query = from placeOwner in _dbContext.AspNetUsers
                                 from place in placeOwner.Place
                                 from offer in place.Offer
                                 from offerTr in offer.OfferTransactions
                                 from delivery in offerTr.UserOfferDelivery.DefaultIfEmpty()
                                 join buyUser in _dbContext.AspNetUsers
                                 on offerTr.BuyUserId equals buyUser.Id
                                 where offerTr.TypeId == (int)BL.Model.OfferTransactionType.Buy
                                 select new
                                 {
                                     PlaceOwner = placeOwner,
                                     Offer = offer,
                                     Delivery = delivery,
                                     OfferTr = offerTr,
                                     BuyUser = buyUser
                                 };

            if (offerTrId != null)
                query = query.Where(t => t.OfferTr.Id == offerTrId);

            if (!string.IsNullOrEmpty(offerOwnerUserId))
                query = query.Where(t => t.PlaceOwner.Id == offerOwnerUserId);

            if (!string.IsNullOrEmpty(buyUserId))
                query = query.Where(t => t.OfferTr.BuyUserId == buyUserId);

            if (placeId != null)
                query = query.Where(t => t.Offer.PlaceId == placeId.Value);

            if (onlyNotDelivered != null)
                query = query.Where(t => t.Delivery == null);

            var dbPurchase = await query.Select(t => new
            {
                PlaceOwnerUserId = t.PlaceOwner.Id,
                UserId = t.BuyUser.Id,
                UserName = t.BuyUser.UserName,
                CreateDate = t.OfferTr.CreateDate,
                OfferTitle = t.Offer.Title,
                OfferId = t.Offer.Id,
                PlaceId = t.Offer.PlaceId,
                TransactionId = t.OfferTr.Id,
                Amount = Math.Abs(t.OfferTr.Amount),
                OfferDescription = t.Offer.Description,
                OfferLogo = t.Offer.LogoUrl
            }).ToListAsync().ConfigureAwait(false);
            
            var purshase = dbPurchase.Select(t => new Purchase()
            {
                UserId = t.UserId,
                UserName = t.UserName,
                Offer = new OfferInfo()
                {
                    Id = t.OfferId,
                    PlaceId = t.PlaceId,
                    Title = t.OfferTitle,
                    Description = t.OfferDescription,
                    LogoUrl = t.OfferLogo
                },
                OfferTransactionId = t.TransactionId,
                Amount = t.Amount,
            }).ToList();

            return purshase;
        }

        public Task UpdateAsync(BL.Model.Offer offer)
        {
            if (offer == null) throw new ArgumentNullException(nameof(offer));

            var dbOffer = _mapper.Map<DL.Offer>(offer);
            _dbContext.Entry(dbOffer).State = EntityState.Modified;
            _dbContext.Entry(dbOffer).Property(t => t.Price).IsModified = false;
            _dbContext.Entry(dbOffer).Property(t => t.PlaceId).IsModified = false;

            return _dbContext.SaveChangesAsync();
        }
    }
}
