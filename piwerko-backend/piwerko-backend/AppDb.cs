using System;
using Jdforsythe.MySQLConnection;

namespace Piwerko.Api
{
    public class AppDb
    {
        public MySQLConn Connection;

        public AppDb()
        {
            Connection = new MySQLConn("server=127.0.0.1;user id=root;pwd='';database=billenium;");
        }

        public MySQLConn sQLConn()
        {
            return Connection;
        }

    }
}