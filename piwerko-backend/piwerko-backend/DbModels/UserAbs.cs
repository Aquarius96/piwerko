using System;
using System.Collections.Generic;
using System.Text;

namespace Piwerko.Data.DbModels
{
    public class UserAbs
    {
        private string Login { get; set; }
        private string Password { get; set; }

        public UserAbs()
        {

        }

        public UserAbs(string login_, string password_)
        {
            this.Login = login_;
            this.Password = password_;
        }
    }
}
