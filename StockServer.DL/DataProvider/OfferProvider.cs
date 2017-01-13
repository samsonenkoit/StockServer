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

        public async Task<IList<BL.Model.Offer>> GetOffersInAreaAsync(Geolocation geolocation, double radiusMetres, int limit)
        {
            var radiusSm = radiusMetres * 1000;
            DbGeography area = GeographyHelper.PointFromGeoPoint(geolocation).Buffer(radiusSm);

            var dbOffers = await (from place in _dbContext.Place
                               from offer in place.Offer
                               where SqlSpatialFunctions.Filter(place.GeoPoint, area) == true
                               select new
                               {
                                   Id = offer.Id,
                                   Title = offer.Title,
                                   Description = offer.Description,
                                   Price = offer.Price,
                                   IsActive = offer.IsActive,
                                   PlaceId = offer.PlaceId,
                                   AvailableAmount = offer.OfferTransactions.Select(t => t.Amount).DefaultIfEmpty(0).Sum()
                               }).Take(limit).ToListAsync();

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

        public async Task<IList<BL.Model.Offer>> GetPlaceOffersAsync(string userId, int placeId)
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

        public async Task<IList<Purchase>> GetPurchaseAsync(string userId, int? placeId)
        {
            var offersInfQuery = from user in _dbContext.AspNetUsers
                                 join offerTr in _dbContext.OfferTransactions on user.Id equals offerTr.BuyUserId
                                 join delivery in _dbContext.UserOfferDelivery.DefaultIfEmpty(null) on offerTr.Id equals delivery.OfferTransactionId
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
                                 };
            if (!string.IsNullOrEmpty(userId))
                offersInfQuery = offersInfQuery.Where(t => t.UserId == userId);

            if (placeId != null)
                offersInfQuery = offersInfQuery.Where(t => t.PlaceId == placeId.Value);

            var offersInf = await offersInfQuery.ToListAsync();

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
