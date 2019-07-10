using userwebapi.Repositories;
using userwebapi.Models;
using userwebapi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace userwebapi.tests
{
    public class UserUnitTestController
    {
        private UserRepository repository;
        public static DbContextOptions<testDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=855823004.database.windows.net;Database=testDb;UID=system;PWD=jU5dFw39kr5v7tjX;";

        static UserUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<testDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public UserUnitTestController()
        {
            var context = new testDbContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            repository = new UserRepository(context);
        }
        #region GetTests
        [Fact]
        public async void Task_GetUserById_Return_OkResult()
        {
            //Arrange  
            UserController controller = new UserController(repository);
            Int64 userId = 2;

            //Act  
            IActionResult data = await controller.GetUser(userId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetUserById_Return_NotFound()
        {
            //Arrange  
            UserController controller = new UserController(repository);
            Int64 userId = 10;

            //Act  
            IActionResult data = await controller.GetUser(userId);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data);
        }
        
        [Fact]
        public async void Task_GetUserById_Return_BadRequestResult()
        {
            //Arrange  
            UserController controller = new UserController(repository);
            Int64? userId = null;

            //Act  
            IActionResult data = await controller.GetUser(userId);

            //Assert  
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async void Task_GetUserById_MatchResult()
        {
            //Arrange  
            UserController controller = new UserController(repository);
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

        #endregion
    }
}
