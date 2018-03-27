using Jdforsythe.MySQLConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace piwerko_backend
{
    public class UserService
{
    private string ConnectionString = @"Data Source=localhost; Database:billenium; User ID=root; Password=''";

    static string connectionString2 = "server=127.0.0.1; uid=root; pwd=''; database=billenium;";

    MySQLConn sql;

    List<Dictionary<string, string>> tableRows;

    public UserService()
    {
        sql = new MySQLConn(connectionString2);
        sql.Query = "SELECT * FROM users";
        tableRows = sql.selectQuery();

    }


    public List<Dictionary<string, string>> cos()
    {
        return tableRows;
    }

}
}
