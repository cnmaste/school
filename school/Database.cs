using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace school
{
    internal class Database
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default._2ConnectionString);
        
        public void openConn()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        public void closeConn()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public SqlConnection getConn()
        {
            return conn;
        }

    }
}
