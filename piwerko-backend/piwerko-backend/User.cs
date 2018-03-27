using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace piwerko_backend
{
    public class User
    {
        private int id { get; set; }
        private string username { get; set; }
        private string password { get; set; }
        private string firstname { get; set; }
        private string lastname { get; set; }
        private string email { get; set; }
        private string phone { get; set; }
        private string avatar { get; set; }

        public User()
        {

        }
        public User(int id_)
        {
            this.id = id_;
        }
        public User(int id_, string username_, string password_, string firstname_, string lastname_, string email_, string phone_)
        {
            this.id = id_;
            this.username = username_;
            this.password = password_;
            this.firstname = firstname_;
            this.lastname = lastname_;
            this.email = email_;
            this.phone = phone_;
        }
        public User(int id_, string username_, string password_, string firstname_, string lastname_, string email_, string phone_,string avatar_)
        {
            this.id = id_;
            this.username = username_;
            this.password = password_;
            this.firstname = firstname_;
            this.lastname = lastname_;
            this.email = email_;
            this.phone = phone_;
            this.avatar = avatar_;
        }
    }
}
