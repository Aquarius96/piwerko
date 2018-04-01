using System;
using System.Data.SqlClient;
using Jdforsythe.MySQLConnection;

namespace Piwerko.Api.Repo
{
    public class AppDb
    {
        public MySQLConn ConnectionMysql { get; set; }
        public string Issuer { get; set; }  //zleceniodawca najprawdopodobniej front
        public string Audience { get; set; } //odbioraca tez front XD
        public string Key { get; set; }
        public string ConnectionString { get; set; }

        public AppDb()
        {
            ConnectionMysql = new MySQLConn("server=127.0.0.1;user id=root;pwd='';database=billenium;");
            ConnectionString = @"Server = (localdb)\mssqllocaldb; Database = billenium ;uid=root; Trusted_Connection = True; MultipleActiveResultSets = true;ConnectRetryCount = 0 ";//"Server =DESKTOP-6A001AV;;Initial Catalog=Test;Trusted_Connection=True";
            Issuer = "http://localhost:50977";
            Audience = "http://localhost:50977";
            Key = "VisualStudioDajeRakaTakBardzo!";
        }


    }
}