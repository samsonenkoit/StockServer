using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.DL
{
    [DbConfigurationType(typeof(DbConfig))]
    public partial class StockDbEntities : DbContext
    {
        
        public StockDbEntities(string conn) : base(conn)
        {

        }
    }
}
