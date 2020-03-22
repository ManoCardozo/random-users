using Moq;
using System;
using System.Linq;
using NUnit.Framework;
using RandomUser.Domain.Entities;
using System.Collections.Generic;
using RandomUser.Application.Services;
using RandomUser.Repository.UnitOfWork;

namespace RandomUser.Application.Tests.ServiceTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService userService;
        private Mock<IUnitOfWork> uowMock;
        private List<User> userList;

        public UserServiceTests()
        {
            
        }

        [SetUp]
        public void Setup()
        {
            uowMock = new Mock<IUnitOfWork>();

            userService = new UserService(uowMock.Object);

            userList = new List<User>{
                new User
                {
                    UserId = Guid.NewGuid()
                },
                new User
                {
                    UserId = Guid.NewGuid()
                },
                new User
                {
                    UserId = Guid.NewGuid()
                }
            };
        }

        [Test]
        public void Should_Return_User()
        {
            //Arrange
            var userId = Guid.NewGuid();

            uowMock
                .Setup(m => m.UserRepository.Get(userId))
                .Returns(new User { 
                    UserId = userId
                });

            // Act
            var user = userService.Get(userId);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.UserId);
        }

        [Test]
        public void Should_Return_Random_User()
        {
            //Arrange
            uowMock
                .Setup(m => m.UserRepository.GetAll())
                .Returns(userList);

            // Act
            var user = userService.GetRandom();

            // Assert
            Assert.IsNotNull(user);
            Assert.Contains(user.UserId, userList.Select(s => s.UserId).ToList());
        }

        [Test]
        public void Should_Return_List_Of_Users()
        {
            //Arrange
            uowMock
                .Setup(m => m.UserRepository.GetAll())
                .Returns(userList);

            // Act
            var users = userService.GetList();

            // Assert
            Assert.IsNotNull(users);
            Assert.IsNotEmpty(users);
            Assert.AreEqual(users, userList);
        }

        [Test]
        public void Should_Update_User()
        {
            //Arrange
            uowMock
                .Setup(m => m.UserRepository.Update(It.IsAny<User>()));

            // Act
            userService.Update(new User());

            // Assert
            uowMock
                .Verify(m => m.UserRepository.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Update_Should_Throw_ArgumentNullException()
        {
            //Arrange
            uowMock
                .Setup(m => m.UserRepository.Update(null))
                .Throws<ArgumentNullException>();

            // Act
            Assert.Throws<ArgumentNullException>(() => userService.Update(null));

            // Assert
            uowMock
                .Verify(m => m.UserRepository.Update(null), Times.Once);
        }

        [Test]
        public void Should_Delete_User()
        {
            //Arrange
            uowMock
                .Setup(m => m.UserRepository.Delete(It.IsAny<User>()));

            // Act
            userService.Delete(new User());

            // Assert
            uowMock
                .Verify(m => m.UserRepository.Delete(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Delete_Should_Throw_ArgumentNullException()
        {
            //Arrange
            uowMock
                .Setup(m => m.UserRepository.Delete(null))
                .Throws<ArgumentNullException>();

            // Act
            Assert.Throws<ArgumentNullException>(() => userService.Delete(null));

            // Assert
            uowMock
                .Verify(m => m.UserRepository.Delete(null), Times.Once);
        }
    }
}