using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class RateService : IRateService
    {
        private readonly IRateRepository _rateRepository;

        public RateService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public bool Add(double value_, int beerid, int userid)
        {
            return _rateRepository.Add(value_,beerid,userid);
        }
        public bool Update(double value_, int beerid, int userid)
        {
            return _rateRepository.Update(value_,beerid,userid);
        }
        public double GetById(int beerid)
        {
            var all = _rateRepository.GetById(beerid).ToList();

            if (all.Count == 0)
            {
                return 0;
            }
            double suma = 0;

            foreach(var var in all)
            {
                suma += var.value;
            }

            var result = suma / all.Count;

            return Math.Round(result, 2);
        }

        public double Getrate(int beerid, int userid)
        {
            var rate = _rateRepository.GetRate(beerid, userid);
            return rate.value;
        }

        public void ClearByUserId(int user_id)
        {
            _rateRepository.ClearByUserId(user_id);
        }
    }
}
