using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Piwerko.Api.Dto;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.Communication;
using Piwerko.Api.Models.DB;
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
        public IActionResult SendForgotPassword(string email) //do dorobienia 
        {
            var result = _userService.ForgotPassword(email);

            if (result) return Ok("Na wskazanego maila: " + email + " wyslano link do zresetowania haslo");
            return BadRequest("Zly email albo cos z polaczeniem jest nie tak");
        }

        [HttpPost("code")]
        public IActionResult GetConfirmationCodeById([FromBody]JObject data) //id
        {
            int index = data["id"].ToObject<Int32>();
            var user = _userService.GetUserById(index);

            if (user == null) return BadRequest("Brak usera o danym id");
            return Ok(user.ConfirmationCode);
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody]LoginModel loginmodel) // email/username & haslo
        {
            var var = _userService.LogIn(loginmodel);
            if (!_userService.GetUserById(var).isConfirmed) return BadRequest("Uzytkownik nie zostal potwierdzony");
            if (var == -1) return BadRequest("Puste haslo"); //moze front bedzie sprawdzal moze nie
            else if (var == -2) return BadRequest("Bledny login/email");
            else if (var == -3) return BadRequest("Zle haslo");
            else return Ok(jwt.BuildFullUserToken(_userService.GetUserById(var)));


        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody]JObject data) //loggeduser -> id & isAdmin && toremove -> id / do przerobienia na model komunikacji
        {
            User logged = data["loggedData"].ToObject<User>();
            User toremove = data["toremoveData"].ToObject<User>();

        /*
        {
	        "loggedData" :
	        {
		        "id" : 5,
		        "isAdmin" : false
	        },
	        "toremoveData" : 
	        {
		        "id" : 5
		
	        }
        } 
        */

            if (logged.isAdmin)
            {
                _userService.Delete(Convert.ToInt32(toremove.id));
                return Ok("Admin usunal");
            }
            else
            {
                if (logged.id == toremove.id)
                {
                    _userService.Delete(Convert.ToInt32(toremove.id));
                    return Ok("Sam sie usuanles :)");
                }
            }
            return BadRequest("Ups cos poszlo nie tak");
        }

        [HttpPost("update")]
        public IActionResult Update(User user)
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
            if (user.isConfirmed) return jwt.BuildFullUserToken(user);
            else return jwt.BuildUserNotFullToken(user);
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

        [AllowAnonymous]
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
            return BadRequest("Niepoprawny klucz");
        }

        [HttpPost("regi")]
        public IActionResult Register([FromBody] User user) //email & haslo
        {
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
    }
}