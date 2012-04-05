using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace hoachdinhtuonglai
{
    internal static class SqlHelper
    {
        public static readonly string ConnectionString = @"Data Source=112.78.5.85;Initial Catalog=hdtl;Persist Security Info=True;User ID=khoa;Password=123asdzxc";

        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
