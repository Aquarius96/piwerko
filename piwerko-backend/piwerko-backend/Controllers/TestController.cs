﻿using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        private readonly IUserService _userService;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("/update/{userId}")]
        public User Post(int userId, [FromBody]User value)
        {
            if (value == _userService.GetUserById(userId))
            {
                return value;
            }
            return new User { firstname = "makarena" };
        }

        [HttpGet]
        public User Posst()
        {
            return _userService.GetUserById(2);
        }









        //// GET: api/Test
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Test/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Test
        //HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Test/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
