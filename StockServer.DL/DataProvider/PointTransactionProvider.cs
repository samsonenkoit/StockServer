using AutoMapper;
using StockServer.BL.DataProvider.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockServer.BL.Model;

namespace StockServer.DL.DataProvider
{
    public class PointTransactionProvider: IPointTransactionProvider
    {
        private readonly StockDbEntities _dbContext;
        private readonly IMapper _mapper;

        public PointTransactionProvider(StockDbEntities dbContext, IMapper mapper)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            _dbContext = dbContext;
            _mapper = mapper;

#if DEBUG
            _dbContext.Database.Log = (a) => Debug.Write(a);
#endif
        }

        public Task EnrollmentPointsForAuthorizationIfNeedAsync(string userId)
        {
            return Task.Factory.StartNew(() => _dbContext.EnrollmentPointsForActivityIfNeed(userId));
        }

        public Task CreateAsync(PointTransaction transaction)
        {
            DL.PointTransactions pTr = _mapper.Map<DL.PointTransactions>(transaction);
            _dbContext.PointTransactions.Add(pTr);
            return _dbContext.SaveChangesAsync();
        }
    }
}
