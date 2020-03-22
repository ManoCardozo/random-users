using NUnit.Framework;
using RandomUser.Application.Extensions;

namespace RandomUser.Application.Tests.ExtensionTests
{
    [TestFixture]
    public class ByteExtensionTests
    {
        public ByteExtensionTests()
        {

        }

        [Test]
        public void Should_Convert_To_Base64_String()
        {
            // Arrange
            var byteArray = new byte[0];

            // Act
            var base64String = byteArray.ToBase64String();

            // Assert
            Assert.IsNotNull(base64String);
        }
    }
}