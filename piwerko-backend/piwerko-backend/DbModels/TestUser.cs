using System;
using System.Collections.Generic;
using System.Text;

namespace Piwerko.Data.DbModels
{
    public class TestUser
    {
        private int Id { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string Firstname { get; set; }
        private string Lastname { get; set; }
        private string Email { get; set; }
        private string Phone { get; set; }
        private string Avatar { get; set; }

        public TestUser()
        {

        }
        public TestUser(int id_)
        {
            this.Id = id_;
        }
        public TestUser(int id_, string username_, string password_, string firstname_, string lastname_, string email_, string phone_)
        {
            this.Id = id_;
            this.Username = username_;
            this.Password = password_;
            this.Firstname = firstname_;
            this.Lastname = lastname_;
            this.Email = email_;
            this.Phone = phone_;
        }
        public TestUser(int id_, string username_, string password_, string firstname_, string lastname_, string email_, string phone_, string avatar_)
        {
            this.Id = id_;
            this.Username = username_;
            this.Password = password_;
            this.Firstname = firstname_;
            this.Lastname = lastname_;
            this.Email = email_;
            this.Phone = phone_;
            this.Avatar = avatar_;
        }
        public string GG()
        {
            return this.Username;
        }
    }
}
