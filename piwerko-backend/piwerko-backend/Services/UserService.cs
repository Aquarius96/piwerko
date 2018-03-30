using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Piwerko.Data.Interfaces;
using Piwerko.Data.Models;
using Piwerko.Data.Dto;
using Piwerko.Data.DbModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using MySqlConnector.Performance;
using System.Threading.Tasks;
using System.Data;

namespace Piwerko.Data.Services
{
    public class UserService
    {
        private int Id { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string Firstname { get; set; }
        private string Lastname { get; set; }
        private string Email { get; set; }
        private string Phone { get; set; }
        private string Avatar { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public UserService(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `users` (`Username`, `Password`) VALUES (@username, @password);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `BlogPost` SET `Title` = @title, `Content` = @content WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `BlogPost` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@title",
                DbType = DbType.String,
                Value = Title,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@content",
                DbType = DbType.String,
                Value = Content,
            });
        }




    }
}
