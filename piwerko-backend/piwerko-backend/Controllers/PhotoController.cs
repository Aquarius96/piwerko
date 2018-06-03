using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Interfaces;

namespace Piwerko.Api.Controllers
{

    public class PhotoController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IPhotoService _photoService;

        public PhotoController(IHostingEnvironment host, IPhotoService photoService)
        {
            _photoService = photoService;
            _host = host;
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
        [Route("api/{type}/addphoto/{Id}")]
        public IActionResult UploadPhoto(int Id,string type, [FromHeader(Name = "username")] string username, IFormFile file)
        {
            string path = null;

            switch (type)
            {
                case "beer":
                    break;
            }
            return null;
        }
    }
}