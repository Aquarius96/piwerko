using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IUserService
    {
       // User Authenticate(string username, string password);
        User Create(User user);
        User GetUserById(int userId);
        
    }
}
