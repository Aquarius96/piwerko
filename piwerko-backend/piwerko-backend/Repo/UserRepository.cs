﻿using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public User GetUser(string username)
        {
            var user = _context.Users.SingleOrDefault(x => x.username == username);

            return user;
        }


        public bool CheckLogin(string username)
        {
            var user = _context.Users.Any(x => x.username == username);

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
