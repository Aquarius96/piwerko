﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/rate")]
    public class RateController : Controller
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpPost("test")]
        public IActionResult test([FromBody] Rate rate)
        {
            var rate_ = _rateService.Getrate(rate.beerId, rate.userId);
           
            if (rate_ == null)
            {
                if (_rateService.Add(rate.value, rate.beerId, rate.userId)) return Ok(_rateService.GetById(rate.beerId));
                return BadRequest("Blad w polaczeniu");
            }
            if (_rateService.Update(rate.value, rate.beerId, rate.userId)) return Ok(_rateService.GetById(rate.beerId));
            return BadRequest("Blad w polaczeniu");
        }

        [HttpGet("getbeer/{beerId}")]
        public double GetAvg(int beerId)
        {
            return _rateService.GetById(beerId);
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

        [HttpPost("getrate")]
        public IActionResult GetSinglerate([FromBody] Rate rate)
        {
            var rate_ = _rateService.Getrate(rate.beerId, rate.userId);
            if (rate_ == null) return NotFound("nie ma czegos takiego");
            return Ok(rate_.value);

        }
    }
}
