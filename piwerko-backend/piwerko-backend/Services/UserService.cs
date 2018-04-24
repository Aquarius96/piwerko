using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Piwerko.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
            
        }
        public User GetUserByEmail(string email_)
        {
            return _userRepository.GetUserByEmail(email_);

        }
        public bool EmailExist (string email)
        {
            return _userRepository.CheckEmail(email);
        }
        public bool LoginExist (string username)
        {
            return _userRepository.CheckLogin(username);
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
            try
            {

                using (SmtpClient client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "piwerkobuissnes@gmail.com",
                        Password = "zaq1@WSX"
                    };
                    client.Credentials = credential;

                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;

                    var message = new MailMessage();

                    message.To.Add(new MailAddress(user.email));
                    message.From = new MailAddress("piwerkobuissnes@gmail.com");
                    message.Subject = "Przywracanie hasla";
                    message.Body = "jakis tam linku do frontu rzeby przekazac id i code sprawdzic itd ide sapc";
                    //message.Body = "http://localhost:8080/api/User/changepwd/" + user.id + "/" + user.ConfirmationCode + "<br />Przywroc haslo<br />Klucz : " + user.ConfirmationCode + "<br />UserId : " + user.id;
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

        public User Register(User user)
        {

            if (_userRepository.CheckEmail(user.email))
                throw new Exception("Email " + user.email + " is already taken");

            user.ConfirmationCode = Guid.NewGuid().ToString();
            user.isAdmin = false;
            user.isConfirmed = false;
            user.avatar_URL = "https://i.pinimg.com/originals/f7/61/b3/f761b3ae57801975e0a605e805626279.png";


            _userRepository.CreateUser(user);
            _userRepository.Save();

            SendActivationEmail(user);

            return user;
        }



        private bool SendActivationEmail(User user)
        {
            var callbackUrl = "http://localhost:8080/api/User/confirm/" + user.id + "/" + user.ConfirmationCode;
            try
            {

                using (SmtpClient client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "piwerkobuissnes@gmail.com",
                        Password = "zaq1@WSX"
                    };
                    client.Credentials = credential;

                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;

                    var message = new MailMessage();

                    message.To.Add(new MailAddress(user.email));
                    message.From = new MailAddress("piwerkobuissnes@gmail.com");
                    message.Subject = "Witamy w Piwerku!";
                    message.Body = "W celu zakonczenia procesu rejestracji prosimy o potwierdzenie maila klikajac w ten link: <a href=\"" + callbackUrl + "\">Link aktywacyjny</a> <br> Klucz : " + user.ConfirmationCode + "<br />UserId : " + user.id; 
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

        public void Update(User user_)
        {
            _userRepository.UpdateUser(user_);
        }

        public int LogIn(User LoginModel)
        {
            if (String.IsNullOrWhiteSpace(LoginModel.password))
                return -1;

            User user = null;

            if (String.IsNullOrWhiteSpace(LoginModel.username)) user = _userRepository.GetUserByEmail(LoginModel.email);
            else user = _userRepository.GetUser(LoginModel.username);
            
            if (user == null)
                return -2;

            if (user.password != LoginModel.password)
                return -3;

            return Convert.ToInt32(user.id);
        }

        public bool Delete(int id)
        {
            return _userRepository.Delete(id);
        }

    }
}
