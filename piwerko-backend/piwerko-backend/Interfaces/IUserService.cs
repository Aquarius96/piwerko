using Piwerko.Api.Models;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IUserService
    {
        User LogIn(User user_);
        User Register(User user);
        User GetUserById(int userId);
        void Update(User user_);
        IEnumerable<User> GetAll();
        User Create(User user);


    }
}
