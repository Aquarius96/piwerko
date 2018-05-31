using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Piwerko.Api.Controllers
{

    public class PhotoController : Controller
    {
        private readonly IHostingEnvironment _host;

        public PhotoController(IHostingEnvironment host)
        {
            _host = host;
        }

        //GET api/file/id
        [HttpGet]
        [Route("api/photo/beers/{fileName}")]
        public IActionResult GetBeer(string fileName)
        {
            if (fileName == null || fileName == "null")
                return NotFound();

            var stream = _host.WebRootPath + "\\beers\\" + fileName;
            var imageFileStream = System.IO.File.OpenRead(stream);
            return File(imageFileStream, "image/jpeg");
        }

        //GET api/file/id
        [HttpGet]
        [Route("api/photo/breweries/{fileName}")]
        public IActionResult GetBrewery(string fileName)
        {
            if (fileName == null || fileName == "null")
                return NotFound();

            var stream = _host.WebRootPath + "\\breweries\\" + fileName;
            var imageFileStream = System.IO.File.OpenRead(stream);
            return File(imageFileStream, "image/jpeg");
        }

    }
}