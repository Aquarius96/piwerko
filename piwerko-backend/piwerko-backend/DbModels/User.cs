using System;
using System.Collections.Generic;
using System.Text;

namespace Piwerko.Data.DbModels
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        
    }
}
