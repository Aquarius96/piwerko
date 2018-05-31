using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.Communication;
using Piwerko.Api.Models.DB;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/brewery")]
    public class BreweryController : Controller
    {
        private readonly IBreweryService _breweryService;
        private readonly IUserService _userService;

        private readonly IHostingEnvironment _host;
        private readonly PhotoSettings _photoSettings;

        public BreweryController(IBreweryService breweryService, IUserService userService, IHostingEnvironment host, IOptions<PhotoSettings> options)
        {
            _breweryService = breweryService;
            _userService = userService;
            _photoSettings = options.Value;
            _host = host;
        }

        [HttpPost]
        [Route("addphoto/{breweryId}")]
        public async Task<IActionResult> UploadPhoto(int breweryId, [FromHeader(Name = "username")] string username, IFormFile file)
        {
            var brewery = _breweryService.GetBreweryById(breweryId);
            if (brewery == null) return NotFound("Brak browaru o danym id");
            if (!_userService.isAdmin(username) && !brewery.added_by.Equals(username)) return BadRequest("Nie jesteś upowaznieony");
            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, _photoSettings.DirOfBrewery);
            await _breweryService.UploadPhoto(breweryId, file, uploadsFolderPath);
            return Ok(_breweryService.GetBreweryById(breweryId));
        }

        [HttpGet("get/confirmed")]
        public IActionResult GetBreweryConfirmed()
        {
            var result = _breweryService.GetAll();
            if (result == null) return NotFound("Pusta lista");
            return Ok(result);
        }

        [HttpGet("getbyid/{breweryId}")]
        public IActionResult GetBreweryById(int breweryId)
        {
            var result = _breweryService.GetBreweryById(breweryId);
            if (result == null) return NotFound("Brak browaru o danym id");
            return Ok(result);

        }

        [HttpGet("getbyname/{breweryName}")]
        public IActionResult GetBreweryByName(string breweryName)
        {
            var result = _breweryService.GetBreweryByName(breweryName);
            if (result == null) return NotFound("Brak browaru o danej nazwie");
            return Ok(result);
        }

        [HttpGet("get/unconfirmed")]
        public IActionResult GetBreweryUnconfirmed()
        {
            var result = _breweryService.GetBreweryUnconfirmed();
            if (result == null) return NotFound("Brak niepotwierdzonych browarow");
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Brewery brewery, [FromHeader(Name = "username")] string username)
        {
            var user = _userService.GetUserByUsername(username);

            if (user == null) return NotFound("Nie znaleziono uzytkownika");
            brewery.isConfirmed = user.isAdmin;
            brewery.added_by = username;
            var result = _breweryService.Add(brewery);
            if (result == null) return BadRequest("Blad przy dodawaniu browaru");
            return Ok(result);
        }

        /*
        [HttpPost("addbyadmin")]
        public IActionResult AddByAdmin([FromBody]BreweryModel data) //do zamiany na modele komunikacji
        {
            User user = _userService.GetUserById(data.user_id);
            if (user == null) return NotFound("Brak usera o danym id");
            Brewery brewery = data.GetBrewery(); //isConfirmed = Tru -> juz ustawione
            brewery.added_by = user.username;
            if (!user.isAdmin) return BadRequest("nie jestes adminem ");
            var result = _breweryService.Add(brewery); 
            if (result == null) return BadRequest("blad przy dodawaniu browaru");
            return Ok(result);
        }
        */
            
        [HttpPost("confirm")]
        public IActionResult Confirm([FromBody]JObject data)
        {
            int breweryId = data["id"].ToObject<Int32>();
            var brewery = _breweryService.GetBreweryById(breweryId);
            brewery.isConfirmed = true;
            _breweryService.Update(brewery);
            return Ok(brewery);
        }

        [HttpPut("update")]
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
            return NotFound("brak browaru o danym id");
        }
    }
}
