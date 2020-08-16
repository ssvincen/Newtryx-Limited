using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtryx.BI
{
    public class ConnectionManager : IConnectionManager
    {
        public IDbConnection NewtryxConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["NewtryxDB"].ConnectionString);
        }
    }
}
