﻿using Piwerko.Api.Interfaces;
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
            _context.Rates.Add(rate);
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
    }
}
