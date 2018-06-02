using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
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

        public IEnumerable<Beer> GetSimilary(int alco, int temp, int ibu, Beer beer)
        {
            Console.WriteLine("temp " + temp);
            var result = _context.Beers.Where(x => Math.Abs(x.alcohol - beer.alcohol) <= 5 && Math.Abs(x.ibu - beer.ibu) <= 5 && Math.Abs(x.servingTemp - temp) <= 5);
            return result;
        }

        public IEnumerable<Beer> GetAll()
        {
            var beer = _context.Beers.Where(x => x.isConfirmed == true);
            return beer;
        }

        public Beer GetBeerById(int id)
        {
            var beer = _context.Beers.SingleOrDefault(x => x.id == id);

            return beer;
        }

        public IEnumerable<Beer> GetBeerByName(string name)
        {
            var beer = _context.Beers.Where(x => x.name == name); //SingleOrDefault(x => x.name == name);

            return beer;
        }

        public IEnumerable<Beer> GetBeerUnconfirmed()
        {
            var beer = _context.Beers.Where(x => x.isConfirmed == false); //SingleOrDefault(x => x.name == name);

            return beer;
        }

        public void Add(Beer beer)
        {
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
