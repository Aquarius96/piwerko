using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Piwerko.Api.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryRepository _breweryRepository;

        public BreweryService(IBreweryRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }

        public IEnumerable<Brewery> GetAll()
        {
            return _breweryRepository.GetAll();
        }
        public Brewery GetBreweryById(int breweryId)
        {
            return _breweryRepository.GetBreweryById(breweryId);
            
        }
        public IEnumerable<Brewery> GetBreweryByName(string name_)
        {
            return _breweryRepository.GetBreweryByName(name_);

        }

        public Brewery Add(Brewery brewery)
        {
            _breweryRepository.Add(brewery);
            _breweryRepository.Save();

            return brewery;
        }

        public void Update(Brewery brewery_)
        {
            _breweryRepository.Update(brewery_);
        }


        public bool Delete(int id)
        {
            return _breweryRepository.Delete(id);
        }

    }
}
