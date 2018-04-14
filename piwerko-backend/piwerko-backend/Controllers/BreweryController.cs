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
    [Route("api/Brewery")]
    public class BreweryController : Controller
    {
        private readonly IBreweryService _breweryService;

        public BreweryController(IBreweryService breweryService)
        {
            _breweryService = breweryService;
        }
        
        [HttpGet("get/confirmed")]
        public IActionResult GetBreweryConfirmed()
        {
            var result = _breweryService.GetAll();
            if (result == null) return BadRequest("Pusta lista");
            return Ok(result);
        }

        [HttpGet("getbyid/{breweryId}")]
        public IActionResult GetBreweryById(int breweryId)
        {
            var result = _breweryService.GetBreweryById(breweryId);
            if (result == null) return BadRequest("Brak browaru o danym id");
            return Ok(result);

        }

        [HttpGet("getbyname/{breweryName}")]
        public IActionResult GetBreweryByName(string breweryName)
        {
            var result = _breweryService.GetBreweryByName(breweryName);
            if (result == null) return BadRequest("Brak browaru o danej nazwie");
            return Ok(result);
        }

        [HttpGet("get/unconfirmed")]
        public IActionResult GetBreweryUnconfirmed()
        {
            var result = _breweryService.GetBreweryUnconfirmed();
            if (result == null) return BadRequest("Brak niepotwierdzonych browarow");
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Brewery brewery)
        {
            brewery.isConfirmed = false;
            var result = _breweryService.Add(brewery);
            if (result == null) return BadRequest("blad przy dodawaniu browaru");
            return Ok(result);
        }

        [HttpPost("addbyadmin")]
        public IActionResult AddByAdmin([FromBody]JObject data)
        {
            User user = data["userData"].ToObject<User>();
            Brewery brewery = data["breweryData"].ToObject<Brewery>();

            if (!user.isAdmin) return BadRequest("nie jestes adminem ");
            brewery.isConfirmed = true;
            var result = _breweryService.Add(brewery); // marcin przerob - usun add by admin i w browarach tez
            if (result == null) return BadRequest("blad przy dodawaniu browaru");
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("confirm/{breweryId}")]
        public IActionResult Confirm(int breweryId)
        {
            var brewery = _breweryService.GetBreweryById(breweryId);
            brewery.isConfirmed = true;
            _breweryService.Update(brewery);
            return Ok(brewery);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Brewery brewery_)
        {
            _breweryService.Update(brewery_);

            return Ok(brewery_);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody]JObject data)
        {
            int index = data["id"].ToObject<Int32>();
            if (_breweryService.Delete(index)) return Ok();
            return BadRequest("brak browaru o danym id");
        }
    }
}
