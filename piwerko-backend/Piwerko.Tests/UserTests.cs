using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Piwerko.Api.Controllers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using Piwerko.Api.Services;
using Xunit;
using Piwerko.Api.Repo;
using Piwerko.Api.Helpers;
using Piwerko.Api.Models.Communication;

namespace Piwerko.Tests
{
    public class UserTests
    {
        // commentuje wszystko bo nie moge dodac migracji ogolnie to wszystko jest do naprawy no prawie wszystko 
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

        /*  [Fact]
          public void Test2() //register / register do zmiany
          {
              var registerModel = new RegisterModel { email = "marzap96@wp.pl", password = "asdsdasd" };
              var userRepository = new Mock<IUserRepository>();
              userRepository.Setup(e => e.EmailExist(registerModel.email)).Returns(false);
              var userService = new UserService(userRepository.Object);
              //var result = userController.
              //var userController = new UserController(userService);
              var result = userService.Register(registerModel);
             // Assert.False(user.isConfirmed);
          }

          [Fact]
          public void Test3() //confirm email / register do zmiany
          {
              var loginModel = new LoginModel { email = "marzap96@wp.pl", password = "asdsdasd" };
              var userRepository = new Mock<IUserRepository>();
              userRepository.Setup(e => e.EmailExist(loginModel.email)).Returns(false);

              var userService = new UserService(userRepository.Object);
              var registeredUser = userService.Register(loginModel);
              userRepository.Setup(e => e.GetUserById(It.IsAny<int>())).Returns(registeredUser);
              var userController = new UserController(userService);
              var result = userController.ConfirmEmail(user); // zamieni³em w nawiasie bo visual mi krzyczal ze 2 argumenty to za duzo
              Assert.IsType<OkObjectResult>(result);
          }*/

        /*
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
    public void Test5() //sign in successfully/ cos jest zle w kontrolerze, chyba nie sprawdza poprawnie czy uzytkownik jest potwierdzony
    {
        var loginModel = new LoginModel {username = "marcinXD", password = "zaq"};
        var user = new User { id = 2, username = "marcinXD", password = "zaq", firstname = "zaq", lastname = "zaq", email = "zaq", phone = "zaq", avatar_URL = "zaq", isAdmin = false, isConfirmed = true };
        var repo = new Mock<IUserRepository>();
        var service = new UserService(repo.Object);
        var controller = new UserController(service);
        repo.Setup(e => e.GetUserByEmail(loginModel.email)).Returns(user);
        repo.Setup(e => e.GetUser(loginModel.username)).Returns(user);
        repo.Setup(e => e.GetUserById(Convert.ToInt32(user.id))).Returns(user);

        var result = controller.SignIn(loginModel);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Test6() //sign with with wrong password/ cos jest zle w kontrolerze, chyba nie sprawdza poprawnie czy uzytkownik jest potwierdzony
    {
        var loginModel = new LoginModel {username = "marcinXD", password = "zaqWSX" };
        var user = new User { id = 2, username = "marcinXD", password = "zaq", firstname = "zaq", lastname = "zaq", email = "zaq", phone = "zaq", avatar_URL = "zaq", isAdmin = false, isConfirmed = true };
        var repo = new Mock<IUserRepository>();
        var service = new UserService(repo.Object);
        var controller = new UserController(service);
        repo.Setup(e => e.GetUserByEmail(loginModel.email)).Returns(user);
        repo.Setup(e => e.GetUser(loginModel.username)).Returns(user);
        repo.Setup(e => e.GetUserById(Convert.ToInt32(user.id))).Returns(user);

        var result = controller.SignIn(loginModel);
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Zle haslo",badRequest.Value);
    }

    [Fact]
    public void Test7() //sign with with wrong email/username / cos jest zle w kontrolerze, chyba nie sprawdza poprawnie czy uzytkownik jest potwierdzony
    {
        var loginModel = new LoginModel { username = "badlogin", password = "zaq" };
        var user = new User { id = 2, username = "marcinXD", password = "zaq", firstname = "zaq", lastname = "zaq", email = "zaq", phone = "zaq", avatar_URL = "zaq", isAdmin = false, isConfirmed = true };
        var repo = new Mock<IUserRepository>();
        var service = new UserService(repo.Object);
        var controller = new UserController(service);
        repo.Setup(e => e.GetUserByEmail(loginModel.email)).Returns((User)null);
        repo.Setup(e => e.GetUser(loginModel.username)).Returns((User)null);
        repo.Setup(e => e.GetUserById(Convert.ToInt32(user.id))).Returns(user);

        var result = controller.SignIn(loginModel);
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Bledny login/email", badRequest.Value);
    }

    [Fact]
    public void Test8() //sign in with user not confirmed/ cos jest zle w kontrolerze, chyba nie sprawdza poprawnie czy uzytkownik jest potwierdzony
    {
        var loginModel = new LoginModel { username = "marcinXD", password = "zaq" };
        var user = new User { id = 2, username = "marcinXD", password = "zaq", firstname = "zaq", lastname = "zaq", email = "zaq", phone = "zaq", avatar_URL = "zaq", isAdmin = false, isConfirmed = true };
        var repo = new Mock<IUserRepository>();
        var service = new UserService(repo.Object);
        var controller = new UserController(service);
        repo.Setup(e => e.GetUserByEmail(loginModel.email)).Returns(user);
        repo.Setup(e => e.GetUser(loginModel.username)).Returns(user);
        repo.Setup(e => e.GetUserById(Convert.ToInt32(user.id))).Returns(user);

        var result = controller.SignIn(loginModel);
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Uzytkownik nie zostal potwierdzony", badRequest.Value);
    }

    [Fact]
    public void Test9()
    {

        var context = new Mock<DataContext>();
        var repo = new UserRepository(context.Object);
        var service = new UserService(repo);
        var controller = new UserController(service);

    }*/

    }
}
