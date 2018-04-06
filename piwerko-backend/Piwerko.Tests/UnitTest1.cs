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
        public void ForgotPassword()
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
        public void Confirm()
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
    }
}

