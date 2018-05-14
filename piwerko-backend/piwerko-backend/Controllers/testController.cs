using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Piwerko.Api.Helpers;
using Piwerko.Api.Models;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    public class testController : Controller
    {

        private IHostingEnvironment _host;

        public testController(IHostingEnvironment host)
        {
            _host = host;
        }
        
        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (file != null) return Ok();
            return BadRequest("bad file");
        }
        /*
        private async Task UploadPhoto(IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //var eventt = await _repository.GetAsync(eventId);
            var test = new PhotoDto
            {
                PhotoUrl = $"{fileName}"
            };
            //_mapper.Map(test, eventt);
            //await _repository.SaveAsync();
        }
        /*
        [HttpPost("ble")]
        public IActionResult Get([FromHeader(Name = "id")] string data, [FromBody]JObject dat)
        {
            var c = dat["id"].ToObject<Int32>();
            return Ok(data + c);
        }*/

    }
}