using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using System.Threading.Tasks;

namespace Piwerko.Api.Controllers
{

    public class PhotoController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IPhotoService _photoService;
        private readonly IBreweryService _breweryService;
        private readonly PhotoSettings _photoSettings;
        private readonly IBeerService _beerService;
        private readonly IUserService _userService;

        public PhotoController(IBreweryService breweryService, IBeerService beerService, IUserService userService, IHostingEnvironment host, IPhotoService photoService, IOptions<PhotoSettings> options)
        {
            _photoService = photoService;
            _photoSettings = options.Value;
            _host = host;
            _beerService = beerService;
            _userService = userService;
            _breweryService = breweryService;
        }

        //GET api/file/id
        [HttpGet]
        [Route("api/photo/{dir}/{fileName}")]
        public IActionResult GetBeer(string fileName, string dir)
        {
            if (fileName == null || fileName == "null")
                return NotFound();

            var stream = _host.WebRootPath + "\\" + dir + "\\" + fileName;
            var imageFileStream = System.IO.File.OpenRead(stream);
            return File(imageFileStream, "image/jpeg");
        }

        [HttpPost]
        [Route("api/test/{type}/addphoto/{Id}")]
        public IActionResult UploadPhoto(int Id,string type, [FromHeader(Name = "username")] string username, IFormFile file)
        {
            switch (type)
            {
                /*
                case "beer":
                    var beer = _beerService.GetBeerById(Id);
                    if (beer == null) return NotFound("Brak piwa o danym id");
                    if (!_userService.isAdmin(username) && !beer.added_by.Equals(username)) return BadRequest("Nie jesteś upowaznieony");
                    beer.photo_URL = @"http://localhost:8080/api/photo/" + _photoSettings.DirOfBeer + "/" + _photoService.SavePhotoToDB(_photoSettings.DirOfBeer, file);
                    break;
                case "brewery":
                    var brewery = _breweryService.GetBreweryById(Id);
                    if (brewery == null) return NotFound("Brak browaru o danym id");
                    if (!_userService.isAdmin(username) && !brewery.added_by.Equals(username)) return BadRequest("Nie jesteś upowaznieony");
                    brewery.photo_URL = @"http://localhost:8080/api/photo/" + _photoSettings.DirOfBrewery + "/" + _photoService.SavePhotoToDB(_photoSettings.DirOfBrewery, file);
                    break;
                case "user":
                    var user = _userService.GetUserById(Id);
                    if (user == null) return NotFound("Brak userna o tym nicku");
                    if (!_userService.isAdmin(username) && user.username != username) return BadRequest("Nie jesteś upowaznieony");
                    user.avatar_URL = @"http://localhost:8080/api/photo/" + _photoSettings.DirOfAvatar + "/" + _photoService.SavePhotoToDB(_photoSettings.DirOfAvatar, file).Result.IsError;
                    break;
                   */
                default:
                    return BadRequest("zly adres strony czy cos");
            }



            return null;
        }
    }
}