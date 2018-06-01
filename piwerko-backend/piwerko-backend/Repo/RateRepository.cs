using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Piwerko.Api.Repo
{
    public class RateRepository : IRateRepository
    {
        private readonly DataContext _context;

        public RateRepository(DataContext context)
        {
            _context = context;
        }

        public bool Add(double value_,int beerid,int userid)
        {
            var rate = new Rate { value = value_, beerId = beerid, userId = userid };
            Console.WriteLine("dodaje");
            Console.WriteLine(rate.value);
            Console.WriteLine(rate.beerId);
            Console.WriteLine(rate.userId);
            _context.Rates.Add(rate);
            _context.SaveChanges();
            return true;
        }
        public  bool Update(double newvalue, int beerid, int userid)
        {
            var rate = _context.Rates.Where(x => x.userId == userid && x.beerId == beerid).SingleOrDefault();
            rate.value = newvalue;
            try
            {
                _context.Rates.Update(rate);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
            return true;
        }
        public IEnumerable<Rate> GetById(int beerid)
        {
            var rates = _context.Rates.Where(x => x.beerId == beerid);
            return rates;
        }

        public Rate GetRate(int beerid, int userid)
        {
            var rates = _context.Rates.Where(x => x.beerId == beerid && x.userId==userid).SingleOrDefault();
            return rates;
        }

        public void ClearByUserId(int user_id)
        {
            foreach (var var in _context.Rates.Where(x => x.userId == user_id).ToList())
            {
                _context.Rates.Remove(var);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
