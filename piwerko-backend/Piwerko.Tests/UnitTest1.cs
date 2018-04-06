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
using System;
using Xunit;

namespace Piwerko.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
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
        public void Test2()
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
        public void Test3()
        {
            var user = new User { email = "marzap96@wp.pl", password = "asdsdasd" };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(e => e.CheckEmail(user.email)).Returns(false);
            
            var userService = new UserService(userRepository.Object);
            var registeredUser = userService.Register(user);
            userRepository.Setup(e => e.GetUserById(It.IsAny<int>())).Returns(registeredUser);
            var userController = new UserController(userService);
            var result = userController.ConfirmEmail(int.Parse((registeredUser.id).ToString()), registeredUser.ConfirmationCode);
            Assert.IsType<OkResult>(result);
        }
    }
}
