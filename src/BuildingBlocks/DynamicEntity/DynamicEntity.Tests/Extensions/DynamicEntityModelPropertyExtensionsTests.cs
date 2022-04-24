using Xunit;
using DynamicEntity.Extensions;
using Common.Models.DynamicEntity;

namespace DynamicEntity.Tests.Extensions
{
    public class DynamicEntityModelPropertyExtensionsTests
    {
        DynamicEntityModelProperty dynamicEntityPropertyTestingModel = new DynamicEntityModelProperty
        {
            PropertyName = "naMe",
            SystemTypeName = "System.String",
            ValueIndex = 1,
            DatabaseEntityProperty = new DynamicEntityDatabaseProperty
            {
                DatabaseTypeName = default(string),
                Length = default(int),
                IsNotNull = default(bool),
                IsKey = default(bool),
                Comment = default(string),
            }
        };

        [Fact]
        public void GetValidPropertyName_PropertyName_ReturnValidPropertyName()
        {
            //Arrage
            var expected = "Name";

            //Act
            var actual = dynamicEntityPropertyTestingModel.GetValidPropertyName();

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetValidFieldName_PropertyName_ReturnValidFieldName()
        {
            //Arrage
            var expected = "_name";

            //Act
            var actual = dynamicEntityPropertyTestingModel.GetValidFieldName();

            //Assert
            Assert.Equal(actual, expected);
        }
    }
}
