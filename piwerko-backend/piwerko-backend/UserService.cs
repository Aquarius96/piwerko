using Jdforsythe.MySQLConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Piwerko.Api.Models;

namespace Piwerko.Api
{
    public class UserService
    {
        private AppDb appDb = new AppDb();
        private MySQLConn sql;
        private List<Dictionary<string, string>> tableRows;
        private List<User> users;


        public UserService()
        {
            try
            {
                sql = appDb.sQLConn();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private User DicToUser(Dictionary<string, string> slownik)
        {
            return new User(int.Parse(slownik["id"]), slownik["username"], slownik["password"], slownik["firstname"], slownik["lastname"], slownik["email"], slownik["phone"], slownik["avatar"]);
        }

        public List<Dictionary<string, string>> cos()
        {
            sql.Query = "SELECT * FROM users";
            tableRows = sql.selectQuery();
            return tableRows;
        }

        public List<User> GetAllUsers()
        {
            sql.Query = "SELECT * FROM users";
            tableRows = sql.selectQuery();
            users = new List<User>();
            foreach (var var in tableRows)
            {
                users.Add(DicToUser(var));
            }
            return users;
        }

    }
}
