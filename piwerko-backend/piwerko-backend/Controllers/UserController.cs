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
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private JWT jwt;
        private readonly IRateService _rateService;
        private readonly ICommentService _commentService;

        public UserController(IUserService userService, IRateService rateService, ICommentService commentService)
        {
            _rateService = rateService;
            _commentService = commentService;
            _userService = userService;
            jwt = new JWT();
        }


        [AllowAnonymous]  //tymmczasowe sprawdzenia dla testow poki nie ma frontu
        [HttpGet("confirm/{userId}/{key}")]
        public IActionResult Confirmed(int userId, string key)
        {
            var user = _userService.GetUserById(userId);
            user.isConfirmed = true;
            user.ConfirmationCode = null;
            _userService.Update(user,false);
            return Ok(user);
        }



        //===========================================================================================================================================

        [HttpPost("cleardata")] 
        public IActionResult ClearData([FromBody] ClearDataModel clearData)
        {
            var user_ = _userService.GetUserById(Convert.ToInt32(clearData.id));
            if (user_ == null) return NotFound("Brak usera o danym id");
            if (!_userService.CheckPasswd(clearData.id, user_.salt)) return BadRequest("Podano złe hasło");

            _rateService.ClearByUserId(Convert.ToInt32(user_.id));
            _commentService.ClearByUserId(Convert.ToInt32(user_.id));

            return Ok("Dane powinny być usunięte");
        }

        [HttpPost("forgotpwd")] // fromt dostaje maila z id i key; pobiera ConfirmationCode dzieki id porownuje go z kluczem i zwraca id oraz nowe haslo
        public IActionResult ConfirmNewPwd([FromBody] PasswordModel pass_model)
        {
            var user_ = _userService.GetUserById(Convert.ToInt32(pass_model.id));
            if (user_ == null) return NotFound("Brak usera o danym id");
            if (_userService.CheckPasswd(pass_model.id,user_.salt)) return BadRequest("Nowe hasło nie moze byc takie same jak stare...");
            
            user_.ConfirmationCode = null;
            _userService.Update(user_,true);

            return Ok(user_);
        }


        [HttpGet("forgotpwd/{email}")]
        public IActionResult SendForgotPassword(string email) //do dorobienia  jak bd front
        {
            var result = _userService.ForgotPassword(email);

            if (result) return Ok("Na wskazanego maila: " + email + " wysłano link do zresetowania hasła");
            return BadRequest("Zły email albo coś z połączeniem jest nie tak");
        }
        
        [HttpPost("changepwd")] 
        public IActionResult ChangePassword(PasswordModel passwordModel)
        {
            var user = _userService.GetUserById(passwordModel.id);
            user.password = passwordModel.password;
            _userService.Update(user, true);
            return Ok("Pomyślnie zmieniono hasło");
            
        }

        [HttpPost("checkpwd")]
        public IActionResult CheckPassword(PasswordModel passwordModel)
        {
            return Accepted(_userService.CheckPasswd(passwordModel.id, passwordModel.password));

        }

        [HttpPost("code")]
        public IActionResult GetConfirmationCodeById([FromBody]JObject data) //id
        {
            int index = data["id"].ToObject<Int32>();
            var user = _userService.GetUserById(index);

            if (user == null) return NotFound("Brak usera o danym id");
            return Ok(user.ConfirmationCode);
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody]LoginModel loginmodel) // email/username & haslo
        {
            var var = _userService.LogIn(loginmodel);
            if (var == -1) return BadRequest("Puste hasło"); //moze front bedzie sprawdzal moze nie
            else if (var == -2) return BadRequest("Błedny login/email");
            else if (var == -3) return BadRequest("Złe haslo");
            else if (!_userService.GetUserById(var).isConfirmed) return BadRequest("Użytkownik nie został potwierdzony");
            return Ok(jwt.BuildFullUserToken(_userService.GetUserById(var)));

        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody]RemoveModel data) //loggeduser -> id & isAdmin && toremove -> id / do przerobienia na model komunikacji
        {
            User logged = _userService.GetUserById(data.logged_id);
            User toremove = _userService.GetUserById(data.remove_id);

            if (toremove.isAdmin) return BadRequest("Nie można usunżć admina");

            if (logged.isAdmin)
            {
                _userService.Delete(Convert.ToInt32(toremove.id));
                return Ok("Admin usunął");
            }
            else
            {
                if (logged.id == toremove.id)
                {
                    _userService.Delete(Convert.ToInt32(toremove.id));
                    return Ok("Sam się usunąłeś :)");
                }
            }
            return BadRequest("Ups cos poszło nie tak");
        }

        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            if (_userService.CheckLogin(user.username,Convert.ToInt32(user.id))) return BadRequest("Login zajęty");  
            if (_userService.CheckEmail(user.email, Convert.ToInt32(user.id))) return BadRequest("Email zajęty");
            _userService.Update(user,true);
            return Ok(user);
        }

        [HttpGet("token")]
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

        [HttpGet("token/{userId}")]
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
            if (_userService.LoginExist(user.username)) return BadRequest("Login zajęty");
            _userService.Update(user,false);
            return Ok(user);

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel user) //email & haslo
        {

            var result = _userService.Register(user);

            if (result.IsError)
                return BadRequest(result.Errors);
            return Ok("Wiadomosc wysłano na Twojego maila: " + user.email);


        }
    }
}