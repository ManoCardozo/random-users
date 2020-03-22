using System;
using NUnit.Framework;
using RandomUser.Application.Extensions;

namespace RandomUser.Application.Tests.ExtensionTests
{
    [TestFixture]
    public class DateExtensionTests
    {
        public DateExtensionTests()
        {

        }

        [TestCase(2020, 1, 12, "12-Jan-2020")]
        [TestCase(1987, 12, 2, "02-Dec-1987")]
        [TestCase(2050, 6, 30, "30-Jun-2050")]
        [TestCase(1998, 4, 10, "10-Apr-1998")]
        public void Should_Return_Expected_String(int year, int month, int day, string expected)
        {
            // Arrange
            var actual = new DateTime(year, month, day);

            // Act
            var uiDateString = actual.ToUiDate();

            // Assert
            Assert.IsNotNull(uiDateString);
            Assert.AreEqual(uiDateString, expected);
        }
    }
}