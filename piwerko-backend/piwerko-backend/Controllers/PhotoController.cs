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
        [Route("api/photo/{dir}/{fileName}")]
        public IActionResult GetBeer(string fileName, string dir)
        {
            if (fileName == null || fileName == "null")
                return NotFound();

            var stream = _host.WebRootPath + "\\" + dir + "\\" + fileName;
            var imageFileStream = System.IO.File.OpenRead(stream);
            return File(imageFileStream, "image/jpeg");
        }
    }
}