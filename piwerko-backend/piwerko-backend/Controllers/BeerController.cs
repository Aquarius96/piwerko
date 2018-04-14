using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        
        [HttpGet("get/confirmed")]
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

        [HttpGet("get/unconfirmed")]
        public IActionResult GetBeerUnconfirmed()
        {
            var result = _beerService.GetBeerUnconfirmed();
            if (result == null) return BadRequest("Brak niepotwierdzonych piw");
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Beer beer)
        {
            beer.isConfirmed = false;
            var result = _beerService.Add(beer);
            if (result == null) return BadRequest("blad przy dodawaniu piwa");
            return Ok(result);
        }

        [HttpPost("addbyadmin")]
        public IActionResult AddByAdmin([FromBody]JObject data)
        /*
        {
	        "userData" :
	        {
		        "isAdmin" : true
	        },
	        "beerData" : 
	        {
		        "name" : "harnas",
		        "alcohol" : 3,
		        "ibu" : 2,
		        "breweryId" : 3,
		        "servingTemp" : 4,
		        "type" : "gownowarte",
		        "description" : "dla biedakow"
		
	        }
        }
        */
        {
            User user = data["userData"].ToObject<User>();
            Beer beer = data["beerData"].ToObject<Beer>();

            if (!user.isAdmin) return BadRequest("Podany user nie jest adminem");
            var result = _beerService.AddByAdmin(beer);
            beer.isConfirmed = true;
            if (result == null) return BadRequest("blad przy dodawaniu piwa");
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("confirm/{beerId}")]
        public IActionResult Confirm(int beerId)
        {
            var beer = _beerService.GetBeerById(beerId);
            beer.isConfirmed = true;
            _beerService.Update(beer);
            return Ok(beer);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Beer beer_)
        {
            _beerService.Update(beer_);

            return Ok(beer_);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody]JObject data)
        {
            int index = data["id"].ToObject<Int32>();
            if (_beerService.Delete(index)) return Ok();
            return BadRequest("brak piwa o danym id");
        }
    }
}
