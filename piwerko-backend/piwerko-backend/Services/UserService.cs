using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Piwerko.Api.Dto;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.Communication;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PhotoSettings _photoSettings;

        public UserService(IUserRepository userRepository, IOptions<PhotoSettings> options)
        {
            _userRepository = userRepository;
            _photoSettings = options.Value;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
            
        }

        public bool isAdmin(int id)
        {
            var user = GetUserById(id);
            if (user == null) return false;
            return user.isAdmin;
        }

        public bool isAdmin(string username)
        {
            var user = _userRepository.GetUser(username);
            if (user == null) return false;
            return user.isAdmin;
        }
        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUser(username);

        }

        public User GetUserByEmail(string email_)
        {
            return _userRepository.GetUserByEmail(email_);

        }
        public bool CheckPasswd(int id, string passwd)
        {
            User user = GetUserById(id);

            return (getHash(user.password, user.salt).Equals(getHash(passwd, user.salt)));

        }
        public bool CheckLogin(string username, int id)
        {
            return _userRepository.CheckLogin(username, id);
        }
        public bool CheckEmail(string username, int id)
        {
            return _userRepository.CheckEmail(username, id);
        }
        public bool EmailExist (string email)
        {
            return _userRepository.EmailExist(email);
        }
        public bool LoginExist (string username)
        {
            return _userRepository.LoginExist(username);
        }
        public bool ForgotPassword(string email_) //to trzeba dorobic gdy bedzie front
        {
            var user = _userRepository.GetUserByEmail(email_);
            if (user == null) return false;
            user.ConfirmationCode = Guid.NewGuid().ToString();
            _userRepository.UpdateUser(user);
            return SendForgotEmail(user);
        }

        private bool SendForgotEmail(User user)
        {
            var callbackUrl = "http://localhost:3000/forgot/" + user.id + "/" + user.ConfirmationCode;
            try
            {

                using (SmtpClient client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "bizznessowe.piwo@gmail.com",
                        Password = "zaq1@WSX"
                    };
                    client.Credentials = credential;

                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;

                    var message = new MailMessage();

                    message.To.Add(new MailAddress(user.email));
                    message.From = new MailAddress("bizznessowe.piwo@gmail.com");
                    message.Body = "W celu zakonczenia procesu zmiany hasla prosimy o klikniecie w ten link: <a href=\"" + callbackUrl + "\">Link</a> <br> Klucz : " + user.ConfirmationCode + "<br />UserId : " + user.id;
                    message.Subject = "Przywracanie hasla";

                    message.IsBodyHtml = true;

                    client.Send(message);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ResultDto<User> Register(RegisterModel _user)
        {
            bool flaga = false;
            var result = new ResultDto<User>
            {
                Errors = new List<string>()
            };

            if (_userRepository.EmailExist(_user.email))
                //throw new Exception("Email " + _user.email + " is already taken");
                result.Errors.Add("Email " + _user.email + " is already taken");

            if (_userRepository.LoginExist(_user.username))
                //throw new Exception("Username " + _user.email + " is already taken");
                result.Errors.Add("Username " + _user.email + " is already taken");

            if (result.IsError) return result;

            var user = new User { email = _user.email, username = _user.username, firstname = _user.firstname, phone = _user.phone, lastname = _user.lastname};
            user.ConfirmationCode = Guid.NewGuid().ToString();
            user.salt = getSalt();
            user.password = getHash(_user.password, user.salt);
            user.isAdmin = false;
            user.isConfirmed = false;
            user.avatar_URL = "https://i.pinimg.com/originals/f7/61/b3/f761b3ae57801975e0a605e805626279.png";
            

            _userRepository.CreateUser(user);
            _userRepository.Save();
            

            for (int i = 0; i < 3; i++)
            {
                if (flaga = SendActivationEmail(user)) break;
                Console.WriteLine("proba wyslania maila " + (i + 1) + ") zakonczona niepowodzeniem");
            }

            result.SuccessResult = user;

            if (!flaga) result.Errors.Add("user dodany do bazy danych ale nie wyslano maila z przyczyn nie znanych");
            return result;
        }

        public async Task<User> UploadPhoto(int user_id, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var user = _userRepository.GetUserById(user_id);
            user.avatar_URL = @"http://localhost:8080/api/photo/" + _photoSettings.DirOfAvatar + "/" + $"{fileName}";
            _userRepository.UpdateUser(user);
            return user;

        }

        private bool SendActivationEmail(User user)
        {
            var callbackUrl = "http://localhost:3000/confirm/" + user.id + "/" + user.ConfirmationCode;
            try
            {

                using (SmtpClient client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "bizznessowe.piwo@gmail.com",
                        Password = "zaq1@WSX"
                    };
                    client.Credentials = credential;

                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;

                    var message = new MailMessage();

                    message.To.Add(new MailAddress(user.email));
                    message.From = new MailAddress("bizznessowe.piwo@gmail.com");
                    message.Subject = user.username + " witamy w Piwerku!";
                    message.Body = "W celu zakonczenia procesu rejestracji prosimy o potwierdzenie maila klikajac w ten link: <a href=\"" + callbackUrl + "\">Link aktywacyjny</a> <br> Klucz : " + user.ConfirmationCode + "<br />UserId : " + user.id; 
                    message.IsBodyHtml = true;

                    client.Send(message);

                    Console.WriteLine("wyslano maila");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("activation mail erro: " + e);
                return false;
            }
            return true;
        }

        public void Update(User user_, bool hashuj)
        {
            if (hashuj )user_.password = getHash(user_.password, user_.salt);
            _userRepository.UpdateUser(user_);
        }


        public int LogIn(LoginModel loginModel)
        {
            if (String.IsNullOrWhiteSpace(loginModel.password))
                return -1;

            User user = null;

            if (String.IsNullOrWhiteSpace(loginModel.username)) user = _userRepository.GetUserByEmail(loginModel.email);
            else user = _userRepository.GetUser(loginModel.username);
            
            if (user == null)
                return -2;

            if (user.password != getHash(loginModel.password, user.salt)) 
                return -3;

            return Convert.ToInt32(user.id);
        }

        public bool Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        private static string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public string getHash(string passwd, string salt)
        { 
            using (var sha256 = SHA256.Create())
            {  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwd+salt));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

    }
}
