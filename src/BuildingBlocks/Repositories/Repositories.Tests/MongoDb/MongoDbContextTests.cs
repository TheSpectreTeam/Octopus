namespace Repositories.Tests.MongoDb
{
    public class MongoDbContextTests
    {
        private readonly IMongoDbConfiguration _configuration;

        public MongoDbContextTests()
        {
            _configuration = new MongoDbConfiguration();
        }

        [Fact]
        public void MongoDbContext_Constructor_Success()
        {
            //Arrage

            //Act
            var context = new MongoDbContext<TestEntity>(_configuration);

            //Assert
            Assert.NotNull(context);
        }

        [Fact]
        public void MongoDbContext_InvalidMongoDbConfiguration_ThrowNullReferenceException()
        {
            //Arrage
            var configuration = new MongoDbConfiguration { Port = 1 };

            //Act
            var context = new MongoDbContext<TestEntity>(configuration);
            var actual = () => context.GetMongoCollection();

            //Assert
            Assert.Throws<MongoConfigurationException>(actual);
        }

        [Fact]
        public void MongoDbContext_NullMongoDbConfiguration_ThrowNullReferenceException()
        {
            //Arrage

            //Act
            var actual = () => new MongoDbContext<TestEntity>(null);

            //Assert
            Assert.Throws<NullReferenceException>(actual);
        }

        [Fact]
        public void GetMongoCollection_ValidCollectionName_ReturnCollectionCollection()
        {
            //Arrage

            //Act
            var context = new MongoDbContext<TestEntity>(_configuration);
            var collection = context.GetMongoCollection();

            //Assert
            Assert.NotNull(collection);
        }
    }
}
