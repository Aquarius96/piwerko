using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace piwerko_backend
{
    public class User_abs
    {
        private string login { get; set; }
        private string password { get; set; }

        public User_abs()
        {

        }

        public User_abs(string login_, string password_)
        {
            this.login = login_;
            this.password = password_;
        }
    }
}
