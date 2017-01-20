using StockServer.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.BL.DataProvider.Interface
{
    public interface IPointTransactionProvider
    {
        Task CreateAsync(PointTransaction transaction);
        Task EnrollmentPointsForAuthorizationIfNeedAsync(string userId);
    }
}
