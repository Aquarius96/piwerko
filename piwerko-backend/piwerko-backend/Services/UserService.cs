using Jdforsythe.MySQLConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Piwerko.Api.Models;
using Piwerko.Api.Repo;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace Piwerko.Api.Services
{
    public class UserService
    {
        private AppDb appDb = new AppDb();
        private MySQLConn sql;
        private List<Dictionary<string, string>> tableRows;
        private List<User> users;



        public UserService()
        {
            try
            {
                sql = appDb.Connection;
                GetDictionary();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private User DicToUser(Dictionary<string, string> slownik)
        {
            return new User(int.Parse(slownik["id"]), slownik["username"], slownik["password"], slownik["firstname"], slownik["lastname"], slownik["email"], slownik["phone"], slownik["avatar"]);
        }

        public void GetDictionary()
        {
            sql.Query = "SELECT * FROM users";
            tableRows = sql.selectQuery();
        }

        public String AddUser(User user)
        {

        }
        public List<User> GetAllUsers()
        {
            users = new List<User>();
            foreach (var var in tableRows)
            {
                users.Add(DicToUser(var));
            }
            return users;
        }
        private int FindIndex(int id)
        {
            int index = 0;
            foreach(var var in GetAllUsers())
            {
                if (var.id == id)
                {
                    break;
                }
                index++;
            }
            return index;
        }
        
        public string GetUserToken(int id)
        {
            try
            {
                return BuildToken(GetAllUsers()[FindIndex(id)], appDb.Key, appDb.Issuer, appDb.Audience);
            }
            catch(Exception e)
            {
                return BuildErroToken(e.ToString(), appDb.Key, appDb.Issuer, appDb.Audience);
            }
        }
        public string BuildErroToken(string erro, string Key, string issuer, string audience, DateTime? expirationDate = null)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("error", erro),
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expirationDate,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string BuildToken(User user, string Key, string issuer , string audience, DateTime? expirationDate = null)
        {
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("username", user.username),
                new Claim("email", user.email),
                new Claim("id", user.id.ToString()),

                /*
                new Claim(ClaimTypes.GivenName, user.username), claimtypes daje jakies zjebane ciagi stringow niby wiadomo o co chodzi ale wydluzaja token
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Sid, user.id.ToString()),
                */

            };
            
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expirationDate,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /*
        private string GetHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }*/

    }
}
