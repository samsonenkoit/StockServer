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
    public class UserProvider : IUserProvider
    {
        private readonly StockDbEntities _dbContext;
        private readonly IMapper _mapper;

        public UserProvider(StockDbEntities dbContext, IMapper mapper)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            _dbContext = dbContext;
            _mapper = mapper;

#if DEBUG
            _dbContext.Database.Log = (a) => Debug.Write(a);
#endif
        }

        public async Task<UserInfo> GetInfoAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentOutOfRangeException(nameof(userId));

            var userQuery = await (from us in _dbContext.AspNetUsers
                              where us.Id == userId
                              select new
                              {
                                  Id = us.Id,
                                  Login = us.UserName,
                                  PointsAmount = us.PointTransactions1.Sum(t => t.Amount)
                              }).ToListAsync().ConfigureAwait(false);

            var userDb = userQuery.FirstOrDefault();

            if (userDb == null)
                throw new Exception($"No user with id {userId}");

            var user = new UserInfo()
            {
                Id = userDb.Id,
                Login = userDb.Login,
                PointsAmount = userDb.PointsAmount
            };

            return user;
        }
    }
}
