using Microsoft.IdentityModel.Tokens;
using Piwerko.Api.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Piwerko.Api.Helpers

{
    public class JWT
    {
        private static string Issuer_ = "http://localhost:49635";
        private static string Audience_ = "http://localhost:49635";
        private static string Key_ = "VisualStudioDajeRakaTakBardzo!";

        public JWT()
        {
        }

        public string BuildUserToken(User user, string Key=null, string issuer=null, string audience=null, DateTime? expirationDate = null)
        {
            if (Key == null)
            {
                Key = Key_;
            }
            if (issuer == null)
            {
                issuer = Issuer_;
            }
            if (audience == null)
            {
                audience = Audience_;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id", user.id.ToString()),
                new Claim("username", user.username),
                new Claim("password", user.password),
                new Claim("firstname", user.firstname),
                new Claim("lastname", user.lastname),
                new Claim("email", user.email),
                new Claim("phone", user.phone),
                new Claim("avatar_URL", user.avatar_URL),
                new Claim("isAdmin", user.isAdmin.ToString()),
                new Claim("isConfirmed", user.isConfirmed.ToString()),

            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expirationDate,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
