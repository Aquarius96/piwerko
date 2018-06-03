﻿using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using Piwerko.Api.Models.Communication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Piwerko.Api.Helpers;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/beer")]

    public class BeerController : Controller
    {
        private readonly IBreweryService _breweryService;
        private readonly IBeerService _beerService;
        private readonly IUserService _userService;
        private readonly IRateService _rateService;

        private readonly IHostingEnvironment _host;
        private readonly PhotoSettings _photoSettings;

        private readonly IPhotoService _photoService;

        public BeerController(IBreweryService breweryService, IBeerService beerService, IUserService userService, IPhotoService photoService, IHostingEnvironment host, IRateService rateService, IOptions<PhotoSettings> options)
        {
            _beerService = beerService;
            _breweryService = breweryService;
            _userService = userService;
            _photoSettings = options.Value;
            _host = host;

            _rateService = rateService;

            _photoService = photoService;
        }

        [HttpGet("getbybreweryid/{Id}")]
        public IActionResult GetBeerByBreweryId(int Id)
        {
            var result = _beerService.GetAll().Where(x=>x.breweryId==Id);
            if (result == null) return NotFound("lista pusta");
            return Ok(result);
            
        }

        [HttpGet("top/{count}")]
        public IActionResult GetAllConfir(int count)
        {
            var result = _beerService.GetAll();
            if (result == null) return NotFound("Pusta lista");
            var res = new List<dynamic>();
            foreach (var var in result)
            {
                var rate = _rateService.GetById(Convert.ToInt32(var.id));
                var json = new { Beer = var, rate };
                res.Add(json);
            }
            var sorted = res.OrderByDescending(p => p.rate);
            return Ok(sorted.Take(count));
        }

        [HttpPost]
        [Route("addphoto/{beerId}")]
        public async Task<IActionResult> UploadPhoto(int beerId,[FromHeader(Name = "username")] string username, IFormFile file)
        {
            var beer = _beerService.GetBeerById(beerId);
            if (beer == null) return NotFound("Brak piwa o danym id");
            if (!_userService.isAdmin(username) && !beer.added_by.Equals(username)) return BadRequest("Nie jesteś upowaznieony");
            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, _photoSettings.DirOfBeer);
            await _beerService.UploadPhoto(beerId, file, uploadsFolderPath);
            return Ok(_beerService.GetBeerById(beerId));
        }

        [HttpGet("get/similary/{id}")]
        public IActionResult GetSimilary(int id)
        {
            var result = _beerService.GetSimilary(id);
            if (result == null) return NotFound("Pusta lista");
            return Ok(result);
        }

        [HttpGet("get/all")]
        public IActionResult GetAll()
        {
            var result = _beerService.GetAll();
            var res = new List<dynamic>();
            foreach (var var in result)
            {
                var rate = _rateService.GetById(Convert.ToInt32(var.id));
                var json = new { Beer = var, Rate = rate };
                res.Add(json);
            }

            var result2 = _beerService.GetBeerUnconfirmed();

            foreach(var var in result2)
            {
                res.Add(var);
            }

            if (res == null) return NotFound("Pusta lista");
            return Ok(res);
        }


        [HttpGet("get/confirmed")]
        public IActionResult GetAllConfir()
        {
            var result = _beerService.GetAll();
            if (result == null) return NotFound("Pusta lista");
            var res = new List<dynamic>();
            foreach (var var in result)
            {
                var rate = _rateService.GetById(Convert.ToInt32(var.id));
                var bro =_breweryService.GetBreweryById(var.breweryId);
                var json = new { Beer = var, rate , String = bro.name};
                res.Add(json);
            }
            return Ok(res);
        }

        [HttpGet("getbyid/{beerId}")]
        public IActionResult GetBeerById(int beerId)
        {
            var result = _beerService.GetBeerById(beerId);
            if (result == null) return NotFound("Brak piwa o danym id");
            var rate = _rateService.GetById(Convert.ToInt32(result.id));
            var bro = _breweryService.GetBreweryById(result.breweryId);
            var json = new { Beer = result, rate , String = bro.name};
            return Ok(json);
        }


        [HttpGet("getbyname/{beerName}")]
        public IActionResult GetBeerByName(string beerName)
        {
            var result = _beerService.GetBeerByName(beerName);
            if (result == null) return NotFound("Brak piwa o danej nazwie");
            return Ok(result);
        }

        [HttpGet("get/unconfirmed")]
        public IActionResult GetBeerUnconfirmed()
        {
            var result = _beerService.GetBeerUnconfirmed();
            if (result == null) return NotFound("Brak niepotwierdzonych piw");
            var res = new List<dynamic>();

            foreach (var var in result)
            {
                var bro = _breweryService.GetBreweryById(var.breweryId);
                var json = new { Beer = var, String = bro.name};
                res.Add(json);
            }

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Beer beer, [FromHeader(Name = "username")] string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user == null) return NotFound("Nie znaleziono uzytkownika");
            beer.added_by = user.username;
            beer.isConfirmed = user.isAdmin;
            var result = _beerService.Add(beer);
            if (result == null) return BadRequest("blad przy dodawaniu piwa");
            return Ok(result);
            /*
            User user = _userService.GetUserById(data.user_id);

            if (user == null) return NotFound("Brak usera o danym id");
            Beer beer = data.GetBeer();
            beer.added_by = user.username;
            beer.isConfirmed = true; // do usuniecia po testach
            var result = _beerService.Add(beer);
            if (result == null) return BadRequest("blad przy dodawaniu piwa");
            return Ok(result);
            */
        }
        /*
        [HttpPost("addbyadmin")]
        public IActionResult AddByAdmin([FromBody]BeerModel data) 
        
        {
            User user = _userService.GetUserById(data.user_id);
            if (user == null) return NotFound("Brak usera o danym id");
            Beer beer = data.GetBeer_Admin();//isConfirmed = Tru -> juz ustawione

            if (!user.isAdmin) return BadRequest("Podany user nie jest adminem");
            beer.added_by = user.username;
            var result = _beerService.Add(beer);
            if (result == null) return BadRequest("blad przy dodawaniu piwa");
            return Ok(result);
        }
        */
        [HttpPost("confirm")]
        public IActionResult Confirm([FromBody]JObject data, [FromHeader(Name = "user_id")] int user_id)
        {
            if (!_userService.isAdmin(user_id)) return BadRequest("nie jestes adminem");
            int beerId = data["id"].ToObject<Int32>();
            var beer = _beerService.GetBeerById(beerId);
            beer.isConfirmed = true;
            _beerService.Update(beer);
            return Ok(beer);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] Beer beer_)
        {
            _beerService.Update(beer_);

            return Ok(beer_);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody]JObject data, [FromHeader(Name = "user_id")] int user_id)
        {
            if (!_userService.isAdmin(user_id)) return BadRequest("nie jestes adminem");
            int index = data["id"].ToObject<Int32>();
            if (_beerService.Delete(index)) return Ok("Pomyslnie usunieto.");
            return NotFound("brak piwa o danym id");
        }
    }
}
