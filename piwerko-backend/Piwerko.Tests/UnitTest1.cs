using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Piwerko.Api.Controllers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using Piwerko.Api.Services;
using Xunit;
using Piwerko.Api.Repo;
using Piwerko.Api.Helpers;

namespace Piwerko.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1() //forgot password
        {
            var user = new User { username = "User", email = "marzap96@wp.pl", password = "asdsdasd" };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(e => e.GetUserByEmail(user.email)).Returns(user);
            var userService = new UserService(userRepository.Object);
            //var userController = new UserController(userService);
            //var result = userController.
            var result = userService.ForgotPassword(user.email);
            Assert.True(result);
        }

        [Fact]
        public void Test2() //register
        {
            var user = new User { email = "marzap96@wp.pl", password = "asdsdasd" };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(e => e.CheckEmail(user.email)).Returns(false);
            var userService = new UserService(userRepository.Object);
            //var result = userController.
            //var userController = new UserController(userService);
            var result = userService.Register(user);
            Assert.False(user.isConfirmed);
        }

        [Fact]
        public void Test3() //confirm email
        {
            var user = new User { email = "marzap96@wp.pl", password = "asdsdasd" };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(e => e.CheckEmail(user.email)).Returns(false);
            
            var userService = new UserService(userRepository.Object);
            var registeredUser = userService.Register(user);
            userRepository.Setup(e => e.GetUserById(It.IsAny<int>())).Returns(registeredUser);
            var userController = new UserController(userService);
            var result = userController.ConfirmEmail(user); // zamieniłem w nawiasie bo visual mi krzyczal ze 2 argumenty to za duzo
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public void Test4() //getJWT
        {
            var jwt = new JWT();
            var user = new User { id = 2, username = "marcinXD", password = "zaq", firstname = "zaq", lastname = "zaq", email = "zaq", phone = "zaq", avatar_URL = "zaq", isAdmin = false, isConfirmed = true};
            
            var repo = new Mock<IUserRepository>();
            var service = new UserService(repo.Object);
            var con = new UserController(service);
            repo.Setup(e => e.GetUserById(It.IsAny<int>())).Returns(user);
            var result = con.GetById(2);
            // marcin popraw to XD


            Assert.Equal(jwt.BuildFullUserToken(user), result);

        }
        [Fact]
        public void Test5() //sign in successfully
        {
            //var loginModel = new User { username = "marcinXD", password = "zaq"};
            var user = new User { id = 2, username = "marcinXD", password = "zaq", firstname = "zaq", lastname = "zaq", email = "zaq", phone = "zaq", avatar_URL = "zaq", isAdmin = false, isConfirmed = true };
            var repo = new Mock<IUserRepository>();
            var service = new UserService(repo.Object);
            var controller = new UserController(service);
            repo.Setup(e => e.GetUserByEmail(user.email)).Returns(user);
            repo.Setup(e => e.GetUser(user.username)).Returns(user);
            repo.Setup(e => e.GetUserById(Convert.ToInt32(user.id))).Returns(user);

            var result = controller.SignIn(user);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
