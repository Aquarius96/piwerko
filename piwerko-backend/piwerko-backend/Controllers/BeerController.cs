using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Helpers;
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
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _beerService.GetAll();
            if (result == null) return BadRequest("Pusta lista");
            return Ok(result);
        }

        [HttpGet("get/{beerId}")]
        public IActionResult GetBeerById(int beerId)
        {
            var result = _beerService.GetBeerById(beerId);
            if (result == null) return BadRequest("Brak piwa o danym id");
            return Ok(result);

        }

        [HttpGet("get/{beerName}")]
        public IActionResult GetBeerByName(string beerName)
        {
            var result = _beerService.GetBeerByName(beerName);
            if (result == null) return BadRequest("Brak piwa o danej nazwie");
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Beer beer)
        {
            var result = _beerService.Add(beer);
            if (result == null) return BadRequest("blad przy dodawaniu piwa");
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Beer beer_)
        {
            _beerService.Update(beer_);

            return Ok(beer_);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Index index)
        {
            if (_beerService.Delete(index.value)) return Ok();
            return BadRequest("brak piwa o danym id");
        }
    }
}
