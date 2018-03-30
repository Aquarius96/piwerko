using System;
using Jdforsythe.MySQLConnection;

namespace Piwerko.Api.Repo
{
    public class AppDb
    {
        public MySQLConn Connection { get; set; }
        public string Issuer { get; set; }  //zleceniodawca najprawdopodobniej front
        public string Audience { get; set; } //odbioraca tez front XD
        public string Key { get; set; }

        public AppDb()
        {
            Connection = new MySQLConn("server=127.0.0.1;user id=root;pwd='';database=billenium;");
            Issuer = "http://localhost:50977";
            Audience = "http://localhost:50977";
            Key = "VisualStudioDajeRakaTakBardzo!";
        }

    }
}