using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Piwerko.Api.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool Delete(long id)
        {
            var user = _context.Users.FirstOrDefault(t => t.id == id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.id == id);

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.email == email);

            return user;
        }

        public User GetUser(string username)
        {
            var user = _context.Users.SingleOrDefault(x => x.username == username);

            return user;
        }


        public bool LoginExist(string username)
        {
            var user = _context.Users.Any(x => x.username == username);

            return user;
        }

        public bool EmailExist(string email)
        {
            var user = _context.Users.Any(x => x.email == email);

            return user;
        }

        public bool CheckLogin(string username, int id)
        {
            var user = _context.Users.Any(x => x.username == username && x.id != id);
            return user;
        }

        public bool CheckEmail(string email, int id)
        {
            var user = _context.Users.Any(x => x.email == email && x.id != id);
            return user;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }

        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
