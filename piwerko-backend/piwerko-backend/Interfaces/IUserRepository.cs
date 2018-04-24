using Piwerko.Api.Models.DB;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        bool CheckLogin(string username);
        void CreateUser(User user);
        void Save();
        User GetUser(string username);
        IEnumerable<User> GetAll();
        void UpdateUser(User user);
        bool Delete(long id);
        bool CheckEmail(string email);
        User GetUserByEmail(string email_);
    }
}
