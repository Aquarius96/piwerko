using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Rate")]
    public class RateController : Controller
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet("get/{userId}")] 
        public double GetAvg(int Id)
        {
            return _rateService.GetById(Id);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Rate rate)
        {
            if (_rateService.Update(rate.value, rate.beerId, rate.userId)) return Ok(_rateService.GetById(rate.beerId));
            return BadRequest("Blad w polaczeniu");
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Rate rate)
        {
            if (_rateService.Add(rate.value, rate.beerId, rate.userId)) return Ok(_rateService.GetById(rate.beerId));
            return BadRequest("Blad w polaczeniu");
        }
    }
}
