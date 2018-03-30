using System;
using System.Collections.Generic;
using System.Text;
using Jdforsythe.MySQLConnection;
using Piwerko.Data.DbModels;

namespace Piwerko.Data.Services
{
    public class TestService
    {
        private string ConnectionString = @"Data Source=localhost; Database:billenium; User ID=root; Password=''";

        static string connectionString2 = "server=127.0.0.1; uid=root; pwd=''; database=billenium;";

        private MySQLConn sql;

        private List<Dictionary<string, string>> tableRows;
        private List<TestUser> users;


        public TestService()
        {
            try
            {
                sql = new MySQLConn(connectionString2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private TestUser DicToUser(Dictionary<string, string> slownik)
        {
            return new TestUser(int.Parse(slownik["id"]), slownik["username"], slownik["password"], slownik["firstname"], slownik["lastname"], slownik["email"], slownik["phone"], slownik["avatar"]);
        }

        public List<Dictionary<string, string>> cos()
        {
            sql.Query = "SELECT * FROM users";
            tableRows = sql.selectQuery();
            return tableRows;
        }

        public List<TestUser> GetAllUsers()
        {
            sql.Query = "SELECT * FROM users";
            tableRows = sql.selectQuery();
            users = new List<TestUser>();
            foreach (var var in tableRows)
            {
                users.Add(DicToUser(var));
            }
            return users;
        }
    }
}
