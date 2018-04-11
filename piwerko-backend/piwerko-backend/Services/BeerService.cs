using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Piwerko.Api.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;

        public BeerService(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public IEnumerable<Beer> GetAll()
        {
            return _beerRepository.GetAll();
        }
        public Beer GetBeerById(int beerId)
        {
            return _beerRepository.GetBeerById(beerId);
            
        }
        public IEnumerable<Beer> GetBeerByName(string name_)
        {
            return _beerRepository.GetBeerByName(name_);

        }

        public Beer Add(Beer beer)
        {
            _beerRepository.Add(beer);
            _beerRepository.Save();

            return beer;
        }

        public void Update(Beer beer_)
        {
            _beerRepository.Update(beer_);
        }


        public bool Delete(int id)
        {
            return _beerRepository.Delete(id);
        }

    }
}
