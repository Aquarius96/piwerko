using Piwerko.Api.Models;

namespace Piwerko.Api.Interfaces
{
    public interface IUserService
    {
        User LogIn(User user_);
        User Register(User user);
        User GetUserById(int userId);
        void Update(User user_);


    }
}
