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
            if (file != null) return Ok(file.Name);
            return BadRequest("bad file");
        }
        
        [HttpPost("ble")]
        public IActionResult Get([FromHeader(Name = "id")] string data, [FromBody]JObject dat)
        {
            var c = dat["id"].ToObject<Int32>();
            return Ok(data + c);
        }

    }
}