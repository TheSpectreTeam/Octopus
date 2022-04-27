using Xunit;
using DynamicEntity.Extensions;

namespace DynamicEntity.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void CapitalizedString_WordInBadCase_ReturnCamelCaseString()
        {
            //Arrage
            var message = "iTEm";
            var expected = "Item";

            //Act
            var actual = message.CapitalizedString();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
