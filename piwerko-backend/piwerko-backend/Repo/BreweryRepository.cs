using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Piwerko.Api.Repo
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly DataContext _context;

        public BreweryRepository(DataContext context)
        {
            _context = context;
        }

        public bool Delete(long id)
        {
            var brewery = _context.Breweries.FirstOrDefault(t => t.id == id);
            if (brewery == null)
            {
                return false;
            }

            _context.Breweries.Remove(brewery);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Brewery> GetAll()
        {
            var brewery = _context.Breweries.Where(x => x.isConfirmed == true);
            return brewery;
        }

        public Brewery GetBreweryById(int id)
        {
            var brewery = _context.Breweries.SingleOrDefault(x => x.id == id);

            return brewery;
        }

        public IEnumerable<Brewery> GetBreweryByName(string name)
        {
            var brewery = _context.Breweries.Where(x => x.name == name); //SingleOrDefault(x => x.name == name);

            return brewery;
        }

        public IEnumerable<Brewery> GetBreweryUnconfirmed()
        {
            var brewery = _context.Breweries.Where(x => x.isConfirmed == false); //SingleOrDefault(x => x.name == name);

            return brewery;
        }


        public void Add(Brewery brewery)
        {
            brewery.isConfirmed = false;
            _context.Breweries.Add(brewery);
        }

        public void Update(Brewery brewery)
        {
            try
            {
                _context.Breweries.Update(brewery);
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
