using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string avatar_URL { get; set; }
        public bool isAdmin { get; set; }
        public bool isConfirmed { get; set; }

        public User()
        {

        }
        public User(int id_)
        {
            this.id = id_;
            this.isAdmin = false;
            this.isConfirmed = false;
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
            this.isAdmin = false;
            this.isConfirmed = false;
        }
        public User(int id_, string username_, string password_, string firstname_, string lastname_, string email_, string phone_, string avatar_)
        {
            this.id = id_;
            this.username = username_;
            this.password = password_;
            this.firstname = firstname_;
            this.lastname = lastname_;
            this.email = email_;
            this.phone = phone_;
            this.avatar_URL = avatar_;
            this.isAdmin = false;
            this.isConfirmed = false;
        }

    }
}
