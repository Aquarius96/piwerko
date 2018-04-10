using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Piwerko.Api.Repo
{
    public class BeerRepository : IBeerRepository
    {
        private readonly DataContext _context;

        public BeerRepository(DataContext context)
        {
            _context = context;
        }

        public bool Delete(long id)
        {
            var beer = _context.Beers.FirstOrDefault(t => t.id == id);
            if (beer == null)
            {
                return false;
            }

            _context.Beers.Remove(beer);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Beer> GetAll()
        {
            return _context.Beers.ToList();
        }

        public Beer GetBeerById(int id)
        {
            var beer = _context.Beers.SingleOrDefault(x => x.id == id);

            return beer;
        }

        public Beer GetBeerByName(string name)
        {
            var beer = _context.Beers.SingleOrDefault(x => x.name == name);

            return beer;
        }



        /*public bool CheckLogin(string username)
        {
            var user = _context.Users.Any(x => x.username == username);

            return user;
        }

        public bool CheckEmail(string email)
        {
            var user = _context.Users.Any(x => x.email == email);

            return user;
        }*/

        public void Add(Beer beer)
        {
            beer.isConfirmed = false;
            _context.Beers.Add(beer);
        }

        public void Update(Beer beer)
        {
            try
            {
                _context.Beers.Update(beer);
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
