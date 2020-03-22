using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using RandomUser.Domain.Entities;
using RandomUser.Application.Extensions;
using RandomUser.Domain.ValueObjects.ListFilter;
using RandomUser.Domain.ValueObjects.ListFilter.Enum;

namespace RandomUser.Application.Tests.ExtensionTests
{
    [TestFixture]
    public class IEnumerableExtensionTests
    {
        private List<User> userList;

        public IEnumerableExtensionTests()
        {

        }

        [SetUp]
        public void Setup()
        {
            userList = new List<User>{
                new User
                {
                    UserId = Guid.NewGuid(),
                    Title = null,
                    FirstName = "Alissa",
                    LastName = "Werner",
                    DateOfBirth = new DateTime(1973, 12, 8),
                    PhoneNumber = "(585) 507-7953"
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Title = "Doctor",
                    FirstName = "Kylie",
                    LastName = "Garner",
                    DateOfBirth = new DateTime(1985, 4, 23),
                    PhoneNumber = "(357) 685-0739"
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Title = null,
                    FirstName = "Layla",
                    LastName = "Rios",
                    DateOfBirth = new DateTime(2013, 9, 9),
                    PhoneNumber = "(653) 487-2520"
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Title = null,
                    FirstName = "Janelle",
                    LastName = "Lyons",
                    DateOfBirth = new DateTime(1970, 2, 16),
                    PhoneNumber = "(260) 656-5757"
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Title = "Professor",
                    FirstName = "Ramon",
                    LastName = "Garza",
                    DateOfBirth = new DateTime(2015, 10, 5),
                    PhoneNumber = "(399) 873-4472"
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Title = null,
                    FirstName = "Blake",
                    LastName = "Lane",
                    DateOfBirth = new DateTime(2006, 6, 13),
                    PhoneNumber = "(385) 273-6131"
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Title = null,
                    FirstName = "Conor",
                    LastName = "Mcmahon",
                    DateOfBirth = new DateTime(1985, 5, 30),
                    PhoneNumber = "(945) 365-8503"
                }
            };
        }

        [TestCase("FirstName", FilterOperator.Equals, "Blake")]
        [TestCase("LastName", FilterOperator.Equals, "Werner")]
        [TestCase("PhoneNumber", FilterOperator.Contains, "(357) 685-0739")]
        public void Should_Return_One_Mathing_User_Record(string columnName, FilterOperator filterOperator, string columnValue)
        {
            // Arrange
            var filterCriteria = new FilterCriteria()
            {
                Options = new List<Filter>
                {
                    new Filter
                    {
                        Name = columnName,
                        Operator = filterOperator,
                        Value = columnValue
                    }
                } 
            };

            // Act
            var results = userList
                .ToPaged(filterCriteria)
                .ToList();

            // Assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(columnValue, results[0].GetType().GetProperty(columnName).GetValue(results[0], null).ToString());
        }

        [TestCase("User", FilterOperator.Contains, "Ga")]
        [TestCase("User", FilterOperator.Contains, "3")]
        public void Should_Return_Users_Records_With_Any_Matching_Value(string columnName, FilterOperator filterOperator, string columnValue)
        {
            // Arrange
            var filterCriteria = new FilterCriteria()
            {
                Options = new List<Filter>
                {
                    new Filter
                    {
                        Name = columnName,
                        Operator = filterOperator,
                        Value = columnValue
                    }
                }
            };

            // Act
            var results = userList
                .ToPaged(filterCriteria)
                .ToList();

            // Assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.True(results.Any(u => u.LastName.Contains(columnValue)) || results.Any(u => u.PhoneNumber.Contains(columnValue)));
        }
    }
}