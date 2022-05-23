using Moq;
using MongoDB.Driver;
using System.Threading;
using System.Net.Http.Json;
using Common.Models.DynamicEntity;
using Repository.MongoDb.Abstractions;
using Loader.Core.Domain.Models;
using Loader.Core.Application.Wrappers;
using Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity;

namespace Loader.FunctionalTests.ApiTests
{
    public class DynamicEntityEndpointsTests
    {
        private readonly Mock<IMongoRepository<LoaderDynamicEntity>> _mockMongoRepository;

        public DynamicEntityEndpointsTests()
        {
            _mockMongoRepository = new Mock<IMongoRepository<LoaderDynamicEntity>>();
        }

        #region GetAllEntitiesAsync

        [Fact]
        public async Task GetAllEntitiesAsync_GetEntitites_SuccessReturn()
        {
            //Arrange
            var dynamicEntities = new List<LoaderDynamicEntity>
            {
                new LoaderDynamicEntity
                {
                    Id = new string('0', 24),
                    EntityName = "TestDynamicEntity",
                    Properties = new List<DynamicEntityModelProperty>()
                },
                new LoaderDynamicEntity
                {
                    Id = new string('1', 24),
                    EntityName = "TestDynamicEntity",
                    Properties = new List<DynamicEntityModelProperty>()
                },
            };

            _mockMongoRepository.Setup(_ => _.GetAllAsync(It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOptions>()))
                .ReturnsAsync(dynamicEntities);

            using var app = new TestLoaderApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act

            var response = await httpClient.GetAsync($"/api/dynamicEntity");
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<Response<IEnumerable<LoaderDynamicEntity>>>(responseText, serializeOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            dynamicEntityResult?.Data.Should().BeEquivalentTo(dynamicEntities);
        }

        #endregion

        #region GetDynamicEntityById

        [Fact]
        public async Task GetDynamicEntityById_ReturnDynamicEntity_SuccessReturn()
        {
            //Arrange
            var id = new string('0', 24);
            var dynamicEntity = new LoaderDynamicEntity
            {
                Id = id,
                EntityName = "TestDynamicEntity",
                Properties = new List<DynamicEntityModelProperty>()
            };

            _mockMongoRepository.Setup(_ => _.GetByIdAsync(
                It.IsAny<object>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOptions>()))
                .ReturnsAsync(dynamicEntity);

            using var app = new TestLoaderApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            ////Act
            var response = await httpClient.GetAsync($"/api/dynamicEntity/{id}");
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<Response<LoaderDynamicEntity>>(responseText, serializeOptions);

            ////Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            dynamicEntityResult?.Data.Should().BeEquivalentTo(dynamicEntity);
        }

        #endregion

        #region AddNewEntityAsync

        [Fact]
        public async Task AddNewEntityAsync_AddNewEntity_Success()
        {
            //Arrange
            var id = new string('1', 24);

            var entity = new LoaderDynamicEntity
            {
                Id = id,
                EntityName = "TestDynamicEntity",
                Properties = new List<DynamicEntityModelProperty>
                {
                    new DynamicEntityModelProperty
                    {
                        PropertyName = "propertyName",
                        SystemTypeName = "systemTypeName"
                    }
                }
            };

            var command = new CreateDynamicEntityCommand()
            {
                EntityName = entity.EntityName,
                Properties = entity.Properties
            };

            _mockMongoRepository.Setup(_ => _.CreateAsync(
                It.IsAny<LoaderDynamicEntity>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            using var app = new TestLoaderApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/dynamicEntity", entity);
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<string>(responseText, serializeOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            dynamicEntityResult.Should().BeEquivalentTo(id);
        }

        #endregion

        #region UpdateEntityAsync

        [Fact]
        public async Task UpdateEntityAsync_UpdateEntity_SuccessUpdated()
        {
            //Arrange
            var id = new string('1', 24);
            var entity = new LoaderDynamicEntity
            {
                Id = id,
                EntityName = "TestDynamicEntity",
                Properties = new List<DynamicEntityModelProperty>
                {
                    new DynamicEntityModelProperty
                    {
                        PropertyName = "propertyName",
                        SystemTypeName = "systemTypeName"
                    }
                }
            };

            _mockMongoRepository.Setup(_ => _.ReplaceOneAsync(
                It.IsAny<LoaderDynamicEntity>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOneAndReplaceOptions<LoaderDynamicEntity, LoaderDynamicEntity>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            using var app = new TestLoaderApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();
            var response = await httpClient.PutAsJsonAsync("/api/dynamicEntity", entity);
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<Response<LoaderDynamicEntity>>(responseText, serializeOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            dynamicEntityResult?.Data.Should().BeEquivalentTo(entity);
        }

        #endregion

        #region DeleteEntityAsync

        [Fact]
        public async Task DeleteEntityAsync_DeleteEntity_SuccessDeleted()
        {
            //Arrange
            var id = new string('1', 24);

            _mockMongoRepository.Setup(_ => _.DeleteByIdAsync(
                It.IsAny<object>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            using var app = new TestLoaderApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();
            var response = await httpClient.DeleteAsync($"/api/dynamicEntity/{id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion
    }
}
