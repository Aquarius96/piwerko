using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using Piwerko.Api.Models.Communication;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Beer")]

    public class BeerController : Controller
    {
        private readonly IBeerService _beerService;
        private readonly IUserService _userService;

        public BeerController(IBeerService beerService, IUserService userService)
        {
            _beerService = beerService;
            _userService = userService;
        }


        [HttpGet("get/confirmed")]
        public IActionResult GetAll()
        {
            var result = _beerService.GetAll();
            if (result == null) return BadRequest("Pusta lista");
            return Ok(result);
        }

        [HttpGet("getbyid/{beerId}")]
        public IActionResult GetBeerById(int beerId)
        {
            var result = _beerService.GetBeerById(beerId);
            if (result == null) return BadRequest("Brak piwa o danym id");
            return Ok(result);

        }

        [HttpGet("getbyname/{beerName}")]
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
        public IActionResult AddByAdmin([FromBody]BeerModel data) // do zaminny na model komunikacji
        
        {
            User user = _userService.GetUserById(data.user_id);
            if (user == null) return BadRequest("Brak usera o danym id");
            Beer beer = data.GetBeer();//isConfirmed = Tru -> juz ustawione

            if (!user.isAdmin) return BadRequest("Podany user nie jest adminem");
            var result = _beerService.Add(beer);
            if (result == null) return BadRequest("blad przy dodawaniu piwa");
            return Ok(result);
        }
        
        [HttpPost("confirm")]
        public IActionResult Confirm([FromBody]JObject data)
        {
            int beerId = data["id"].ToObject<Int32>();
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
            if (_beerService.Delete(index)) return Ok("Pomyslnie usunieto.");
            return BadRequest("brak piwa o danym id");
        }
    }
}
