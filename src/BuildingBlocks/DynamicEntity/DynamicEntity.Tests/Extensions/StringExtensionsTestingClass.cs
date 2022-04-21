using Xunit;
using System;
using DynamicEntity.Extensions;

namespace DynamicEntity.Tests.Extensions
{
    public class StringExtensionsTestingClass
    {
        [Fact]
        public void ToCamelCase_WordInBadCase_ReturnCamelCaseString()
        {
            //Arrage
            var message = "iTEm";
            var expected = "Item";

            //Act
            var actual = message.ToCamelCase();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValidIntProperty_IntValueInString_ReturnIntValue()
        {
            //Arrage
            var message = "101";
            var expected = 101;

            //Act
            var actual = message.GetValidIntProperty();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValidIntProperty_EmptyValue_ReturnDefaultIntValue()
        {
            //Arrage
            var message = "";
            var expected = 0;

            //Act
            var actual = message.GetValidIntProperty();

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetValidIntProperty_NullValue_ReturnDefaultIntValue()
        {
            string message = null;
            var expected = 0;

            //Act
            var actual = message.GetValidIntProperty();

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetValidDateTimeProperty_DateTimeValueInString_ReturnDateTimeValue()
        {
            //Arrage
            var message = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var expected = DateTime.Now.Date;

            //Act
            var actual = message.GetValidDateTimeProperty();

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetValidDateTimeProperty_NullValue_ReturnDefaultDateTimeValue()
        {
            //Arrage
            string message = null;
            var expected = DateTime.MinValue.Date;

            //Act
            var actual = message.GetValidDateTimeProperty();

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetValidDateTimeProperty_EmptyValue_ReturnDefaultDateTimeValue()
        {
            //Arrage
            var message = "";
            var expected = DateTime.MinValue.Date;

            //Act
            var actual = message.GetValidDateTimeProperty();

            //Assert
            Assert.Equal(actual, expected);
        }
    }
}
