﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using userwebapi.Models;
using userwebapi.Controllers;
using userwebapi.Repositories;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace userwebapitests
{
    public class UserControllerUnitTest
    {
        #region GetTests
        [Fact, Trait("Category", "Get")]
        public async void Task_GetUserById_Return_NotFound()
        {
            //Arrange  
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_GetUserById_Return_NotFound));
            var controller = new UserController(new UserRepository(dbContext));
            Int64 userId = 10;

            //Act  
            IActionResult data = await controller.GetUser(userId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact, Trait("Category", "Get")]
        public async void Task_GetUserById_Return_BadRequestResult()
        {
            //Arrange  
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_GetUserById_Return_BadRequestResult));
            var controller = new UserController(new UserRepository(dbContext));
            Int64? userId = null;

            //Act  
            IActionResult data = await controller.GetUser(userId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact, Trait("Category", "Get")]
        public async void Task_GetUserById_MatchResult()
        {
            //Arrange  
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_GetUserById_MatchResult));
            var controller = new UserController(new UserRepository(dbContext));
            Int64 userId = 1;

            //Act  
            IActionResult data = await controller.GetUser(userId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            OkObjectResult okResult = data.Should().BeOfType<OkObjectResult>().Subject;

            User user = okResult.Value.Should().BeAssignableTo<User>().Subject;

            Assert.Equal(1, user.Id);
            Assert.Equal("Ken Thompson", user.Name);

        }
        [Fact, Trait("Category", "Get")]
        public async void Task_GetUsers_Return_Ok()
        {
            //Arrange
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_GetUsers_Return_Ok));
            var controller = new UserController(new UserRepository(dbContext));

            //Act
            IActionResult data = await controller.GetUsers();

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact, Trait("Category", "Get")]
        public async void Task_GetUsers_Return_NotFound()
        {
            //Arrange
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_GetUsers_Return_NotFound));
            var controller = new UserController(new UserRepository(dbContext));

            //Act
            IActionResult data = await controller.GetUsers();

            //Assert
            Assert.IsType<OkObjectResult>(data);
            OkObjectResult okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            List<User> users = okResult.Value.Should().BeAssignableTo<List<User>>().Subject;

            foreach (User user in users)
            {
                await controller.DeleteUser(user.Id);
            }

            data = await controller.GetUsers();
            Assert.IsType<NotFoundResult>(data);

        }
        #endregion

        #region PostTests
        [Fact, Trait("Category", "Post")]
        public async void Task_PostUser_Return_Ok()
        {
            //Arrange
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_PostUser_Return_Ok));
            var controller = new UserController(new UserRepository(dbContext));
            User user = new User()
            {
                Name = "Albert Einstein",
                UserName = "aeinstein",
                BirthDate = new DateTime(1879, 3, 14),
                CreationDate = DateTime.Now,
                IsActive = true,
                PassWordHash = "pwHash",
                PassWordSalt = "pwSalt"
            };

            //Act
            IActionResult data = await controller.AddUser(user);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact, Trait("Category", "Post")]
        public async void Task_PostUser_Return_BadRequestResult()
        {
            //Arrange
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_PostUser_Return_BadRequestResult));
            var controller = new UserController(new UserRepository(dbContext));
            User user = new User()
            {
                Id = 1,
                Name = "",
                UserName = "",
                BirthDate = DateTime.Now,
                CreationDate = DateTime.Now,
                IsActive = true,
                PassWordHash = "pwHash",
                PassWordSalt = "pwSalt"
            };

            //Act
            IActionResult data = await controller.AddUser(user);

            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        #endregion
    }
}