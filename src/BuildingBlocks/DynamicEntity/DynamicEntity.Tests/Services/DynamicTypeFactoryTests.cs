using Xunit;
using System.Linq;
using System.Collections.Generic;
using DynamicEntity.Services;
using DynamicEntity.Extensions;
using DynamicEntity.Abstractions;
using Common.Models.DynamicEntity;

namespace DynamicEntity.Tests.Services
{
    public class DynamicTypeFactoryTests
    {
        IDynamicTypeFactory dynamicTypeFactory = new DynamicTypeFactory(
            dynamicEntityPropertyService: new DynamicEntityPropertyService());

        DynamicEntityModel dynamicEntityTestingModel = new DynamicEntityModel
        {
            EntityName = "Person",
            Properties = new List<DynamicEntityModelProperty>
                {
                    new DynamicEntityModelProperty
                    {
                        PropertyName = "Id",
                        SystemTypeName = "System.Int32",
                        ValueIndex = 0,
                        DatabaseEntityProperty = new DynamicEntityDatabaseProperty
                        {
                            DatabaseTypeName = default(string),
                            Length = default(int),
                            IsNotNull = default(bool),
                            IsKey = default(bool),
                            Comment = default(string),
                        }
                    },
                    new DynamicEntityModelProperty
                    {
                        PropertyName = "Name",
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
                    }
                }
        };

        [Fact]
        public void GetTypeWithDynamicProperty_DynamicEntityModel_ReturnNotNull()
        {
            //Arrage

            //Act
            var actualType = dynamicTypeFactory.GetTypeWithDynamicProperty(dynamicEntityTestingModel);
            //Assert
            Assert.NotNull(actualType);
        }

        [Fact]
        public void GetTypeWithDynamicProperty_DynamicEntityModel_ReturnValidTypeName()
        {
            //Arrage
            var expected = dynamicEntityTestingModel.EntityName;
            //Act
            var actualType = dynamicTypeFactory.GetTypeWithDynamicProperty(dynamicEntityTestingModel);
            //Assert
            Assert.Equal(actualType.Name, expected);
        }

        [Fact]
        public void GetTypeWithDynamicProperty_DynamicEntityModel_TypeContainsDeclaredFields()
        {
            //Arrage

            //Act
            var actualType = dynamicTypeFactory.GetTypeWithDynamicProperty(dynamicEntityTestingModel);
            //Assert
            Assert.NotNull(actualType.GetField(dynamicEntityTestingModel.Properties.ToArray()[0].GetValidFieldName()));
            Assert.NotNull(actualType.GetField(dynamicEntityTestingModel.Properties.ToArray()[1].GetValidFieldName()));
        }

        [Fact]
        public void GetTypeWithDynamicProperty_DynamicEntityModel_TypeContainsDeclaredProperties()
        {
            //Arrage

            //Act
            var actualType = dynamicTypeFactory.GetTypeWithDynamicProperty(dynamicEntityTestingModel);
            //Assert
            Assert.NotNull(actualType.GetProperty(dynamicEntityTestingModel.Properties.ToArray()[0].GetValidPropertyName()));
            Assert.NotNull(actualType.GetProperty(dynamicEntityTestingModel.Properties.ToArray()[1].GetValidPropertyName()));
        }
    }
}
