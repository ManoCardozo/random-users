using Moq;
using System;
using System.Linq;
using NUnit.Framework;
using RandomUser.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RandomUser.Domain.Entities;
using RandomUser.WebUI.Controllers;
using RandomUser.Application.Services;
using RandomUser.Domain.ValueObjects.ListFilter;

namespace RandomUser.WebUI.Tests.ControllerTests
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController controller;
        private Mock<IUserService> userServiceMock;

        public UserControllerTests()
        {

        }

        [SetUp]
        public void Setup()
        {
            userServiceMock = new Mock<IUserService>();
            controller = new UserController(userServiceMock.Object);
        }

        [Test]
        public void Should_Return_User()
        {
            // Arrange
            var userId = Guid.NewGuid();

            userServiceMock
                .Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(new User
                {
                    UserId = userId
                });

            // Act
            var response = controller.Get(userId);
            var okResult = response.Result as OkObjectResult;
            var userViewModel = okResult.Value as UserViewModel;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.AreEqual(userViewModel.UserId, userId);
        }

        [Test]
        public void Should_Return_User_Not_Found()
        {
            // Arrange
            var userId = Guid.NewGuid();

            userServiceMock
                .Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(() => null);

            // Act
            var response = controller.Get(userId);
            var notFoundResult = response.Result as NotFoundResult;

            // Assert
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(notFoundResult.StatusCode, 404);
        }

        [Test]
        public void Should_Return_Random_User()
        {
            // Arrange
            var userId = Guid.NewGuid();

            userServiceMock
                .Setup(m => m.GetRandom())
                .Returns(new User
                {
                    UserId = userId
                });

            // Act
            var response = controller.GetRandom();
            var okResult = response.Result as OkObjectResult;
            var userViewModel = okResult.Value as UserViewModel;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.AreEqual(userViewModel.UserId, userId);
        }

        [Test]
        public void Should_Return_Random_User_Not_Found()
        {
            // Arrange
            var userId = Guid.NewGuid();

            userServiceMock
                .Setup(m => m.GetRandom())
                .Returns(() => null);

            // Act
            var response = controller.GetRandom();
            var notFoundResult = response.Result as NotFoundResult;

            // Assert
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(notFoundResult.StatusCode, 404);
        }

        [Test]
        public void Should_Return_User_List()
        {
            // Arrange
            var filterCriteria = new FilterCriteria()
            {
                Page = 1,
                PageSize = 3
            };

            userServiceMock
                .Setup(m => m.GetList())
                .Returns(new List<User>
                {
                    new User(),
                    new User(),
                    new User(),
                    new User(),
                    new User(),
                    new User(),
                    new User()
                });

            // Act
            var response = controller.GetList(filterCriteria);
            var okResult = response.Result as OkObjectResult;
            var userViewModel = okResult.Value as IEnumerable<UserViewModel>;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.AreEqual(userViewModel.Count(), filterCriteria.PageSize);
        }

        [Test]
        public void Should_Update_User()
        {
            // Arrange
            var userModel = new UserViewModel
            {
                UserId = Guid.NewGuid(),
                Title = "Mr.",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1980, 3, 6),
                PhoneNumber = "(123) 456 7890"
            };

            userServiceMock
                .Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(new User());

            // Act
            var response = controller.Update(userModel);
            var okResult = response as OkResult;

            // Assert

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
        }

        [Test]
        public void Should_Not_Update_Not_Found_User()
        {
            // Arrange
            var userModel = new UserViewModel
            {
                UserId = Guid.NewGuid()
            };

            userServiceMock
                .Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(() => null);

            // Act
            var response = controller.Update(userModel);
            var notFoundResult = response as NotFoundResult;

            // Assert
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(notFoundResult.StatusCode, 404);
        }

        [Test]
        public void Should_Delete_User()
        {
            var userId = Guid.NewGuid();

            userServiceMock
                .Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(new User());

            // Act
            var response = controller.Delete(userId);
            var okResult = response as OkResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
        }

        [Test]
        public void Should_Not_Delete_Not_Found_User()
        {
            // Arrange
            var userId = Guid.NewGuid();

            userServiceMock
                .Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(() => null);

            // Act
            var response = controller.Delete(userId);
            var notFoundResult = response as NotFoundResult;

            // Assert
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(notFoundResult.StatusCode, 404);
        }

    }
}