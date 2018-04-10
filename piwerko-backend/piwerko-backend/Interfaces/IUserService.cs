using Piwerko.Api.Models;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IUserService
    {
        int LogIn(User user_);
        User Register(User user);
        User GetUserById(int userId);
        void Update(User user_);
        IEnumerable<User> GetAll();
        User Create(User user);
        bool Delete(int id);
        bool LoginExist(string username);
        bool ForgotPassword(string email_);
        bool EmailExist(string email);
    }
}
