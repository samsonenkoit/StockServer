using StockServer.BL.DataProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockServer.BL.Model;
using StockServer.DL.Helper;
using AutoMapper;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Spatial;
using System.Diagnostics;

namespace StockServer.DL.DataProvider
{
    public class PlaceProvider : IPlaceProvider
    {
        private readonly StockDbEntities _dbContext;
        private readonly IMapper _mapper;

        public PlaceProvider(StockDbEntities dbContext, IMapper mapper)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            _dbContext = dbContext;
            _mapper = mapper;

#if DEBUG
            _dbContext.Database.Log = (a) => Debug.Write(a);
#endif
        }

        public async Task CreateAsync(string userId, BL.Model.Place place)
        {
            if (place == null) throw new ArgumentNullException(nameof(place));

            DL.Place pl = _mapper.Map<DL.Place>(place);

            var user = new AspNetUsers() { Id = userId };
            pl.AspNetUsers.Add(user);

            _dbContext.AspNetUsers.Attach(user);
            _dbContext.Place.Add(pl);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BL.Model.Place> GetAsync(int id)
        {
            var placesDb = await (from pl in _dbContext.Place
                            where pl.Id == id
                            select pl).ToListAsync().ConfigureAwait(false);

            var placeDb = placesDb.FirstOrDefault();

            if (placesDb == null)
                throw new Exception($"Place with id {id} not found");

            var place = _mapper.Map<BL.Model.Place>(placeDb);

            return place;
        }

        public async Task<IList<BL.Model.Place>> GetForUserAsync(string userId)
        {
            var dbPlaces = await (from place in _dbContext.Place
                                  from user in place.AspNetUsers
                                  where user.Id == userId
                                  select place).ToListAsync();

            var allPlaces = dbPlaces.Select(t => _mapper.Map<BL.Model.Place>(t)).ToList();
            return allPlaces;
        }

        public async Task<IList<PlaceInfo>> GetShortPlaceInAreaAsync(Geolocation geolocation, double radiusMetres, int limit)
        {
            var radiusSm = radiusMetres * 1000;
            DbGeography area = GeographyHelper.PointFromGeoPoint(geolocation).Buffer(radiusSm);

            var pointsQuery = (from place in _dbContext.Place
                               where SqlSpatialFunctions.Filter(place.GeoPoint, area) == true
                               select new { place.Id, place.GeoPoint, place.Name }).Take(limit);

            var points = await pointsQuery.ToListAsync();

            return points.Select(t => new PlaceInfo(t.Id,  t.Name, new Geolocation((double)t.GeoPoint.Latitude, (double)t.GeoPoint.Longitude))).ToList();
        }

        public Task UpdateAsync(BL.Model.Place place)
        {
            if (place == null) throw new ArgumentNullException(nameof(place));

            var dbPlace = _mapper.Map<DL.Place>(place);

            _dbContext.Entry<DL.Place>(dbPlace).State = EntityState.Modified;

            return _dbContext.SaveChangesAsync();
        }
    }
}
