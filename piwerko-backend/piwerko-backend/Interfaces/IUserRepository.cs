using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        bool CheckLogin(string username);
        void CreateUser(User user);
        void Save();
        User GetUser(string username);
    }
}
