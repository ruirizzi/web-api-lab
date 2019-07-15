using System;
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
        public async void Task_GetUserById_Return_OkResult()
        {
            //Arrange  
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_GetUserById_Return_OkResult));
            var controller = new UserController(new UserRepository(dbContext));
            Int64 userId = 1;

            //Act  
            IActionResult data = await controller.GetUser(userId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
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
        public void Task_GetUsers_Return_BadRequestResult()
        {
            //Arrange  
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_GetUsers_Return_BadRequestResult));
            var controller = new UserController(new UserRepository(dbContext));

            //Act  
            var data = controller.GetUsers();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }
        #endregion

        #region PostTests
        [Fact, Trait("Category", "Post")]
        public async void Task_PostUser_Return_OkResult()
        {
            //Arrange
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_PostUser_Return_OkResult));
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
        public async void Task_PostUser_InvalidData_Return_BadRequest()
        {
            //Arrange
            var dbContext = DbContextMocker.GetTestDbContext(nameof(Task_PostUser_InvalidData_Return_BadRequest));
            var controller = new UserController(new UserRepository(dbContext));
            User user = new User();

            //Act
            IActionResult data = await controller.AddUser(user);

            //Assert
            Assert.IsType<BadRequestResult>(data);
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

        #region PutTests
        [Fact, Trait("Category", "Put")]
        public async void Task_PutUser_Return_OkResult()
        {
            //Arrange
            TestDbContext dbContext = DbContextMocker.GetTestDbContext(nameof(Task_PutUser_Return_OkResult));
            UserController controller = new UserController(new UserRepository(dbContext));
            User user = new User() { Name = "Guido van Rossum", UserName = "grossum", BirthDate = new DateTime(1956, 1, 31), IsActive = true, PassWordHash = "psHash", PassWordSalt = "pwSalt", CreationDate = DateTime.Now };

            //Act
            IActionResult existingUser = await controller.AddUser(user);
            OkObjectResult okResult = existingUser.Should().BeOfType<OkObjectResult>().Subject;
            long oldUserId = okResult.Value.Should().BeAssignableTo<long>().Subject;
            user.Id = oldUserId;
            user.UserName = "guidor";

            IActionResult data = await controller.UpdateUser(user);

            //Assert
            Assert.IsType<OkResult>(data);
        }


        [Fact, Trait("Category", "Put")]
        public async void Task_PutUser_Return_MatchData()
        {
            //Arrange
            TestDbContext dbContext = DbContextMocker.GetTestDbContext(nameof(Task_PutUser_Return_MatchData));
            UserController controller = new UserController(new UserRepository(dbContext));
            User user = new User() { Name = "Guido van Rossum", UserName = "grossum", BirthDate = new DateTime(1956, 1, 31), IsActive = true, PassWordHash = "psHash", PassWordSalt = "pwSalt", CreationDate = DateTime.Now };

            //Act
            IActionResult existingUser = await controller.AddUser(user);
            OkObjectResult okResult = existingUser.Should().BeOfType<OkObjectResult>().Subject;
            long oldUserId = okResult.Value.Should().BeAssignableTo<long>().Subject;
            user.Id = oldUserId;
            user.UserName = "guidor";

            IActionResult data = await controller.UpdateUser(user);

            //Assert
            Assert.IsType<OkResult>(data);

            data = await controller.GetUser(user.Id);

            okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            User upatedUser = okResult.Value.Should().BeAssignableTo<User>().Subject;

            Assert.Equal(user.UserName, upatedUser.UserName);
            Assert.Equal(user.Name, upatedUser.Name);
            Assert.Equal(user.Id, upatedUser.Id);
        }

        #endregion
        #region DeleteTests

        [Fact, Trait("Category", "Delete")]
        public async void Task_Delete_User_Return_OkResult()
        {
            //Arrange  
            TestDbContext dbContext = DbContextMocker.GetTestDbContext(nameof(Task_Delete_User_Return_OkResult));
            UserController controller = new UserController(new UserRepository(dbContext));

            //Act  
            IActionResult data = await controller.DeleteUser(1);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact, Trait("Category", "Delete")]
        public async void Task_Delete_User_Return_NotFound()
        {
            //Arrange  
            TestDbContext dbContext = DbContextMocker.GetTestDbContext(nameof(Task_Delete_User_Return_NotFound));
            UserController controller = new UserController(new UserRepository(dbContext));

            //Act  
            IActionResult data = await controller.DeleteUser(5);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact, Trait("Category", "Delete")]
        public async void Task_Delete_User_Return_BadRequest()
        {
            //Arrange  
            TestDbContext dbContext = DbContextMocker.GetTestDbContext(nameof(Task_Delete_User_Return_BadRequest));
            UserController controller = new UserController(new UserRepository(dbContext));

            //Act  
            IActionResult data = await controller.DeleteUser(null);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        #endregion
    }
}
