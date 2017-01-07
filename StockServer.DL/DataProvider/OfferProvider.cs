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

        public async Task CreateAsync(BL.Model.Offer offer)
        {
            if (offer == null) throw new ArgumentNullException(nameof(offer));

            DL.Offer dbOffer = _mapper.Map<DL.Offer>(offer);

            _dbContext.Offer.Add(dbOffer);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IList<BL.Model.Offer>> GetAllForUserAsync(string userId)
        {
            var dbOffers = await (from place in _dbContext.Place
                                  from user in place.AspNetUsers
                                  join offer in _dbContext.Offer on place.Id equals offer.PlaceId
                                  where user.Id == userId
                                  select offer).ToListAsync().ConfigureAwait(false);

            var offers = dbOffers.Select(t => _mapper.Map<BL.Model.Offer>(t)).ToList();
            return offers;
        }
    }
}
