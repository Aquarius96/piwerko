using System;
using Jdforsythe.MySQLConnection;

namespace MySqlConnector.Performance
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection;

        public AppDb()
        {
            Connection = new MySqlConnection("server=127.0.0.1;user id=root;password='';port=3306;database=blog;");
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}