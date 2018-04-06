using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System.Linq;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private JWT jwt;

        public UserController(IUserService userService)
        {
            _userService = userService;
            jwt = new JWT();
        }
        
        [HttpGet("get/{userId}")]
        public string GetById(int userId)
        {
            var user =  _userService.GetUserById(userId);

            return jwt.BuildUserToken(user);
        }

        [Route("regi")]
        [HttpPost]
        public string Register([FromBody] User user)
        {
            if (user == null)
            {
                return "BadRequest()";
            }

            _userService.Register(user);

            return "CreatedAtRoute(GetUser, new { id = user.id }, user)";
        }

        [AllowAnonymous]  //nie wiem co to robi ale ktos tam to mial i chyba potrzebne
        [HttpGet("confirm/{userId}/{key}")]
        public IActionResult ConfirmEmail(int userId, string key)
        {
            var user = _userService.GetUserById(userId);

            if (user.ConfirmationCode == key)
            {
                user.isConfirmed = true;
                user.ConfirmationCode = null;
                _userService.Update(user);
                return Ok();
            }
            return BadRequest();
        }

        [AllowAnonymous]  //nie wiem co to robi ale ktos tam to mial i chyba potrzebne
        [HttpGet("changepwd/{userId}/{key}")]
        public IActionResult ChangePassword(int userId, string key)
        {
            var user = _userService.GetUserById(userId);

            if (user.ConfirmationCode == key)
            {
                user.ConfirmationCode = null;
                _userService.Update(user);
                return Ok();
            }
            return BadRequest();
        }

        //private readonly DataContext _context;

        //public UserController(DataContext context)
        //{
        //    _context = context;

        //    if (_context.Users.Count() == 0)
        //    {
        //        _context.Users.Add(new User { username = "Admin",isAdmin=true,isConfirmed=true,password="zaqwsx" });
        //        _context.SaveChanges();
        //    }
        //}

        //[HttpGet]
        //public IEnumerable<User> GetAll()
        //{
        //    return _context.Users.ToList();
        //}

        //[HttpGet("{id}", Name = "GetId")]
        //public IActionResult GetById(long id)
        //{
        //    var item = _context.Users.FirstOrDefault(x => x.id == id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return new ObjectResult(item);
        //}

        //[HttpPost]
        //public IActionResult Create([FromBody] User user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Users.Add(user);
        //    _context.SaveChanges();

        //    return CreatedAtRoute("GetUser", new { id = user.id }, user);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(long id, [FromBody] User user)
        //{
        //    if (user == null || user.id != id)
        //    {
        //        return BadRequest();
        //    }

        //    var updated = _context.Users.FirstOrDefault(t => t.id == id);
        //    if (updated == null)
        //    {
        //        return NotFound();
        //    }

        //    updated = user;

        //    _context.Users.Update(updated);
        //    _context.SaveChanges();
        //    return new NoContentResult();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(long id)
        //{
        //    var user = _context.Users.FirstOrDefault(t => t.id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    _context.SaveChanges();
        //    return new NoContentResult();
        //}

        //// GET: api/User
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/User/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/User
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/User/5
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
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Piwerko.Api.Models;
//using Piwerko.Api.Repo;

//namespace Piwerko.Api.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/User")]
//    public class UserController : Controller
//    {

//        private readonly DataContext _context;
//        private JWT jwt = new JWT();

//        public UserController(DataContext context)
//        {
//            _context = context;

//            if (_context.Users.Count() == 0)
//            {
//                _context.Users.Add(new User { username = "Admin", isAdmin = true, isConfirmed = true, password = "zaqwsx" });
//                _context.SaveChanges();
//            }
//        }

//        [HttpGet]
//        public string[] GetAll()
//        {
//            List<string> result = new List<string>();
//            var list = _context.Users.ToList();
//            foreach (var var in list)
//            {
//                result.Add(jwt.BuildUserToken(var));
//            }
//            return result.ToArray();
//        }

//        [HttpGet("{id}", Name = "GetId")]
//        public string GetById(long id)
//        {
//            var user = _context.Users.FirstOrDefault(x => x.id == id);
//            if (user == null)
//            {
//                return "NotFound()";
//            }
//            return jwt.BuildUserToken(user);
//        }

//        [HttpPost]
//        public string Create([FromBody] User user)
//        {
//            if (user == null)
//            {
//                return "BadRequest()";
//            }

//            _context.Users.Add(user);
//            _context.SaveChanges();

//            return jwt.BuildUserToken(user);
//        }

//        [HttpPut("{id}")]
//        public string Update(long id, [FromBody] User user)
//        {
//            if (user == null || user.id != id)
//            {
//                return "BadRequest()";
//            }

//            var updated = _context.Users.FirstOrDefault(t => t.id == id);
//            if (updated == null)
//            {
//                return "NotFound()";
//            }

//            updated = user;

//            _context.Users.Update(updated);
//            _context.SaveChanges();
//            return jwt.BuildUserToken(updated);
//        }

//        [HttpDelete("{id}")]
//        public string Delete(long id)
//        {
//            var user = _context.Users.FirstOrDefault(t => t.id == id);
//            if (user == null)
//            {
//                return "NotFound()";
//            }

//            _context.Users.Remove(user);
//            _context.SaveChanges();
//            return "Successful()";
//        }

//        //// GET: api/User
//        //[HttpGet]
//        //public IEnumerable<string> Get()
//        //{
//        //    return new string[] { "value1", "value2" };
//        //}

//        //// GET: api/User/5
//        //[HttpGet("{id}", Name = "Get")]
//        //public string Get(int id)
//        //{
//        //    return "value";
//        //}

//        //// POST: api/User
//        //[HttpPost]
//        //public void Post([FromBody]string value)
//        //{
//        //}

//        //// PUT: api/User/5
//        //[HttpPut("{id}")]
//        //public void Put(int id, [FromBody]string value)
//        //{
//        //}

//        //// DELETE: api/ApiWithActions/5
//        //[HttpDelete("{id}")]
//        //public void Delete(int id)
//        //{
//        //}



//    }
//}
