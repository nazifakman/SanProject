using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SanProject.Repository
{

    //mssql13.trwwww.com
    //db_seilabutik
    //sa_butikseila
    //Qsnf82#0
    public class BaseRepo : IDisposable
    {
        public IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["SanOdev"].ConnectionString);


        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();
        }
    }
}
