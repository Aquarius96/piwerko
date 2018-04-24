using Piwerko.Api.Models.Communication;
using Piwerko.Api.Models.DB;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IUserService
    {
        int LogIn(LoginModel user_);
        User Register(User user);
        User GetUserById(int userId);
        void Update(User user_);
        IEnumerable<User> GetAll();
        bool Delete(int id);
        bool LoginExist(string username);
        bool ForgotPassword(string email_);
        bool EmailExist(string email);
    }
}
