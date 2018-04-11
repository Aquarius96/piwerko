using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Beer")]
    public class BeerController : Controller
    {
        private readonly IBeerService _beerService;

        public BeerController(IBeerService beerService)
        {
            _beerService = beerService;
        }
        //nie mam pojecia co tutaj powinno byc po tym httpget wiec nie tykam tego
        [HttpGet("get/{userId}")]
        public IEnumerable<Beer> GetAll()
        {
            return _beerService.GetAll();
        }

        [HttpGet("get/{beerId}")]
        public Beer GetBeerById(int beerId)
        {
            return _beerService.GetBeerById(beerId);

        }

        [HttpGet("get/{beerName}")]
        public IEnumerable<Beer> GetBeerByName(string name_)
        {
            return _beerService.GetBeerByName(name_);

        }

        [HttpGet("get/{beerName}")]
        public Beer Add(Beer beer)
        {
            _beerService.Add(beer);

            return beer;
        }

        [HttpGet("get/{beerName}")]
        public void Update(Beer beer_)
        {
            _beerService.Update(beer_);
        }

        [HttpGet("get/{beerName}")]
        public bool Delete(int id)
        {
            return _beerService.Delete(id);
        }
    }
}
