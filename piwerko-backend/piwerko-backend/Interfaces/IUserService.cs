using Piwerko.Api.Dto;
using Piwerko.Api.Models.Communication;
using Piwerko.Api.Models.DB;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IUserService
    {
        int LogIn(LoginModel user_);
        ResultDto<User> Register(RegisterModel user);
        User GetUserById(int userId);
        void Update(User user_, bool hashuj);
        IEnumerable<User> GetAll();
        bool Delete(int id);
        bool LoginExist(string username);
        bool ForgotPassword(string email_);
        bool EmailExist(string email);
        bool CheckLogin(string username, int id);
        bool CheckEmail(string username, int id);
        bool CheckPasswd(int id, string passwd);
        bool isAdmin(int id);
        bool isAdmin(string username);
        string getHash(string passwd, string salt);
        User GetUserByUsername(string username);
    }
}
