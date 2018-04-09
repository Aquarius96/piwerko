using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Dto;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
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

        [HttpGet("delA/{userId}")] //tymczasowe usuwanie dla testow
        public bool delbyadmin(int userId)
        {
            return _userService.Delete(userId);
        }

        [AllowAnonymous]  //tymmczasowe sprawdzenia dla testow poki nie ma frontu
        [HttpGet("confirm/{userId}/{key}")]
        public IActionResult COnfirmeD(int userId, string key)
        {
            var user = _userService.GetUserById(userId);
            user.isConfirmed = true;
            user.ConfirmationCode = null;
            _userService.Update(user);
            return Ok(user);
        }



        //===========================================================================================================================================


        [HttpPost("forgotpwd")] // fromt dostaje maila z id i key; pobiera ConfirmationCode dzieki id porownuje go z kluczem i zwraca id oraz nowe haslo
        public IActionResult ConfirmNewPwd([FromBody] User user)
        {
            var user_ = _userService.GetUserById(Convert.ToInt32(user.id));
            if (user_ == null) return BadRequest("Brak usera o danym id");

            if (user_.password == user.password) return BadRequest("Nowe haslo nie moze byc takie same jak stare...");

            user_.password = user.password;
            user_.ConfirmationCode = null;

            return Ok(user);
        }


        [HttpGet("forgotpwd/{email}")]
        public IActionResult SendForgotPassword(string email)
        {
            var result = _userService.ForgotPassword(email);

            if (result) return Ok("Na wskazanego maila: " + email + " wyslano link do zresetowania haslo");
            return BadRequest("Zly email albo cos z polaczeniem jest nie tak");
        }

        [HttpPost("code")]
        public IActionResult GetConfirmationCodeById([FromBody] Index index)
        {
            var user = _userService.GetUserById(index.value);

            if (user == null) return BadRequest("Brak usera o danym id");
            return Ok(user.ConfirmationCode);
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] User user) //    email/username & haslo
        {
            if (user == null)
            {
                return BadRequest("Błedny user");
            }

            var var = _userService.LogIn(user);
            if (var == -1) return BadRequest("Puste haslo");
            else if (var == -2) return BadRequest("Bledny login/email");
            else if (var == -3) return BadRequest("Zle haslo");
            else return Ok(jwt.BuildFullUserToken(_userService.GetUserById(var)));


        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Index index)
        {
            _userService.Delete(index.value);
            return Ok();
        }

        [HttpPost("uptade")]
        public IActionResult Uptade(User user)
        {
            if (_userService.LoginExist(user.username)) return BadRequest("Login zajety");
            if (_userService.EmailExist(user.email)) return BadRequest("Email zajety");
            _userService.Update(user);
            return Ok(user);
        }

        [HttpGet("getall")]
        public List<string> GetAll()
        {
            var result = new List<string>();
            foreach (var var in _userService.GetAll())
            {
                if (var.isConfirmed) result.Add(jwt.BuildFullUserToken(var));
                else result.Add(jwt.BuildUserNotFullToken(var));
            }
            return result;
        }

        [HttpGet("getfull/{userId}")]
        public string GetById(int userId)
        {
            var user =  _userService.GetUserById(userId);

            return jwt.BuildFullUserToken(user);
        }

        [HttpGet("getnotfull/{userId}")]
        public string GetByIdNotFull(int userId)
        {
            var user = _userService.GetUserById(userId);

            return jwt.BuildUserNotFullToken(user);
        }

        

        [AllowAnonymous]  //nie wiem co to robi ale ktos tam to mial i chyba potrzebne
        [HttpPost("confirm")]
        public IActionResult ConfirmEmail(User user)
        {
            /*
             * wczesniej wyslany link do frontu zawierajacy id i ConfirmationCode 
             * front pobiera usera po id
             * sprawdza ConfirmationCode usera z ConfirmationCode liunku jesli sie zgadza to ok uzytkownik wypelni swoje dane
             * jesli nei to wyswietlasz zly link czy cos xd
             * po wykonanym zwraca mi usera do updtae
             */
            if (_userService.LoginExist(user.username)) return BadRequest("Login zajety");
            _userService.Update(user);
            return Ok(user);

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
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost("regi")]
        public IActionResult Register([FromBody] User user) //email & haslo
        {
            if (user == null)
            {
                return BadRequest("Bledny user");
            }

            try
            {
                _userService.Register(user);
                return Ok("Wiadomosc wyslano na twojego maila: "+user.email);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }

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
