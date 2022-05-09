using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories.Tests.MongoDb
{
    public class MongoRepositoryTests
    {
        private List<TestEntity> _mockEntitiesList;

        private Mock<IAsyncCursor<TestEntity>> _mockAsyncCursor;
        private Mock<IMongoDbContext<TestEntity>> _mockMongoContext;
        private Mock<IMongoCollection<TestEntity>> _mockMongoCollection;
        
        private string Id = "569ed8269353e9f4c51617aa";
        private ObjectId _objectId;

        private readonly TestEntity _entity;

        public MongoRepositoryTests()
        {
            _objectId = new ObjectId(Id);

            _entity = new TestEntity
            {
                Id = _objectId,
                Name = "testEntity"
            };

            _mockEntitiesList = new List<TestEntity> { _entity };
            _mockMongoCollection = new Mock<IMongoCollection<TestEntity>>();
            _mockAsyncCursor = new Mock<IAsyncCursor<TestEntity>>();
            _mockMongoContext = new Mock<IMongoDbContext<TestEntity>>();
            _mockMongoContext.Setup(_ => _.GetMongoCollection()).Returns(_mockMongoCollection.Object);

            #region Mock IAsyncCursor

            _mockAsyncCursor
                .Setup(_ => _.Current)
                .Returns(_mockEntitiesList);
            _mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _mockAsyncCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            #endregion

            #region Mock IMongoCollection

            _mockMongoCollection
               .Setup(i => i.InsertOneAsync(It.IsAny<TestEntity>(),
               It.IsAny<InsertOneOptions>(),
               It.IsAny<CancellationToken>()))
               .Callback<TestEntity, InsertOneOptions, CancellationToken>(
               (entity, options, token) =>
               {
                   _mockEntitiesList.Add(entity);
               });
            _mockMongoCollection
                .Setup(i => i.InsertManyAsync(It.IsAny<IEnumerable<TestEntity>>(),
                It.IsAny<InsertManyOptions>(),
                It.IsAny<CancellationToken>()))
                .Callback<IEnumerable<TestEntity>, InsertManyOptions, CancellationToken>(
                (entityList, options, token) =>
                {
                    _mockEntitiesList.AddRange(entityList);
                });
            _mockMongoCollection
                .Setup(i => i.FindAsync(It.IsAny<FilterDefinition<TestEntity>>(),
                It.IsAny<FindOptions<TestEntity, TestEntity>>(),
                It.IsAny<CancellationToken>()))
                .Callback<FilterDefinition<TestEntity>, FindOptions<TestEntity, TestEntity>, CancellationToken>(
                (filter, opt, token) =>
                {
                    var filteredCollection = _mockEntitiesList.Where(filter.GetFunc()).ToList();
                    _mockAsyncCursor.Setup(_ => _.Current).Returns(filteredCollection);
                })
                .ReturnsAsync(_mockAsyncCursor.Object);
            _mockMongoCollection
                .Setup(i => i.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<TestEntity>>(),
                It.IsAny<TestEntity>(),
                It.IsAny<FindOneAndReplaceOptions<TestEntity, TestEntity>>(),
                It.IsAny<CancellationToken>()))
                .Callback<FilterDefinition<TestEntity>, TestEntity, FindOneAndReplaceOptions<TestEntity, TestEntity>, CancellationToken>(
                (filter, newEntity, options, token) =>
                {
                    var filteredCollection = _mockEntitiesList.Where(filter.GetFunc());
                    var entity = filteredCollection.FirstOrDefault();
                    entity.Name = newEntity.Name;
                    _mockAsyncCursor.Setup(_ => _.Current).Returns(filteredCollection);
                })
                .ReturnsAsync(_mockAsyncCursor.Object.FirstOrDefault());
            _mockMongoCollection
                .Setup(i => i.DeleteOneAsync(It.IsAny<FilterDefinition<TestEntity>>(),
                It.IsAny<CancellationToken>()))
                .Callback<FilterDefinition<TestEntity>, CancellationToken>(
                (filter, token) =>
                {
                    var filteredCollection = _mockEntitiesList.Where(filter.GetFunc());
                    var selectedItem = filteredCollection.FirstOrDefault();
                    _mockEntitiesList.Remove(selectedItem);
                });
            _mockMongoCollection
                .Setup(i => i.DeleteManyAsync(It.IsAny<FilterDefinition<TestEntity>>(),
                It.IsAny<CancellationToken>()))
                .Callback<FilterDefinition<TestEntity>, CancellationToken>(
                (filter, token) =>
                {
                    var filteredCollection = _mockEntitiesList.Where(filter.GetFunc());
                    foreach (var item in filteredCollection.ToList())
                    {
                        _mockEntitiesList.Remove(item);
                    }
                });

            #endregion
        }

        #region Create methods tests

        [Fact]
        public async void CreateAsync_AddEntity_Success()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);
            var newEntity = new TestEntity
            {
                Id = new ObjectId(),
                Name = "NewTestEntity"
            };

            //Act
            await repository.CreateAsync(newEntity);
            var result = await repository.GetAllAsync();

            //Assert
            Assert.Contains(newEntity, result);
        }

        [Fact]
        public async void CreateAsync_AddNullValue_ThrowArgumentNullException()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.CreateAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [Fact]
        public async void CreateManyAsync_AddListEntities_Success()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);
            var entitiesList = new List<TestEntity>
            {
                new TestEntity
                {
                    Id = new ObjectId(),
                    Name = "First entity"
                },
                new TestEntity
                {
                    Id = new ObjectId(),
                    Name = "Second entity"
                }
            };

            //Act
            await repository.CreateManyAsync(entitiesList);
            var result = (await repository.GetAllAsync()).ToList();
            var resultCount = result.Count();

            //Assert
            Assert.Equal(3, resultCount);
        }

        [Fact]
        public async void CreateManyAsync_AddNullValue_ThrowArgumentNullException()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.CreateManyAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actual);
        }
        #endregion

        #region Get methods tests

        [Fact]
        public async void GetByIdAsync_FindByValidId_ReturnItem()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var result = await repository.GetByIdAsync(_objectId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_objectId, result.Id);
        }

        [Fact]
        public async void GetByIdAsync_FindByInvalidTypeId_ThrowArgumentException()
        {
            //Arrange
            var objectId = 123;
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.GetByIdAsync(objectId);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(actual);
        }

        [Fact]
        public async void GetByIdAsync_FindByNullId_ThrowArgumentNullException()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.GetByIdAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [Fact]
        public async void GetOneAsync_GetByValidLinqFunc_ReturnItem()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var result = await repository.GetOneAsync(i => i.Id.Equals(_objectId));

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetOneAsync_GetByNullLinqFunc_ReturnItem()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.GetOneAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [Fact]
        public async void GetAllAsync_GetAllItems_ReturnAllItems()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var result = await repository.GetAllAsync();
            var resultCount = result.Count();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, resultCount);
        }

        #endregion

        #region Update methods tests

        [Fact]
        public async void ReplaceOneAsync_ReplaceItemById_SuccessReplace()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);
            var expected = new TestEntity
            {
                Id = _objectId,
                Name = "Updated Entity"
            };

            //Act
            var actual = await repository.ReplaceOneAsync(expected);

            //Assert
            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public async void ReplaceOneAsync_EntityIsNull_ThrowArgumentNullException()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.ReplaceOneAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        #endregion

        #region Delete methods tests

        [Fact]
        public async void DeleteByIdAsync_DeleteByValidId_SuccessDeleted()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            await repository.DeleteByIdAsync(_objectId);
            var actual = await repository.GetAllAsync();

            //Assert
            Assert.DoesNotContain(_entity, actual);
        }

        [Fact]
        public async void DeleteByIdAsync_DeleteByInvalidTypeId_ThrowArgumentException()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);
            var id = 123;

            //Act
            var actual = () => repository.DeleteByIdAsync(id);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(actual);
        }

        [Fact]
        public async void DeleteByIdAsync_DeleteByNullId_ThrowArgumentNullException()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.DeleteByIdAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [Fact]
        public async void DeleteOneAsync_DeleteByFunc_SuccessDeleted()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            await repository.DeleteOneAsync(i => i.Id.Equals(_objectId));
            var actual = await repository.GetAllAsync();

            //Assert
            Assert.DoesNotContain(_entity, actual);
        }

        [Fact]
        public async void DeleteOneAsync_DeleteByNullFunc_ThrowArgumentNullException()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.DeleteOneAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [Fact]
        public async void DeleteManyAsync_DeleteByFunc_SuccessDeleted()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            await repository.DeleteManyAsync(i => i.Id.Equals(_objectId));
            var actual = await repository.GetAllAsync();

            //Assert
            Assert.DoesNotContain(_entity, actual);
        }

        [Fact]
        public async void DeleteManyAsync_DeleteByNullFunc_ThrowArgumentNullException()
        {
            //Arrange
            var repository = new MongoRepository<TestEntity>(_mockMongoContext.Object);

            //Act
            var actual = () => repository.DeleteManyAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        #endregion
    }
}
