using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.
        }
        public User GetUserById(int userId)
        {
            var user = _userRepository.GetUserById(userId);

            return user;
        }

        public User Create(User user)
        {
            if (string.IsNullOrWhiteSpace(user.password))
                throw new Exception("Password is required");

            if (_userRepository.CheckLogin(user.username))
                throw new Exception("Username " + user.username + " is already taken");
            
            _userRepository.CreateUser(user);
            _userRepository.Save();

            return user;
        }

        public User Authenticate(User user_)
        {
            if (string.IsNullOrEmpty(user_.username) || string.IsNullOrEmpty(user_.password))
                return null;

            var user = _userRepository.GetUser(user_.username);

            if (user == null)
                return null;

            if (user.password != user_.password)
                return null;

            return user;
        }

        //public User Authenticate(string username, string password)
        //{
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //        return null;

        //    var user = _userRepository.GetUser(username, password);

        //    if (user == null)
        //        return null;

        //    if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        //        return null;

        //    return user;
        //}

    }
}
