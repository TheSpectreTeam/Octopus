namespace Repository.MongoDb.Context
{
    public class MongoDbContext<T> : IMongoDbContext<T> where T : IMongoEntityBase
    {
        private readonly IMongoDbConfiguration _mongoDatabaseConfiguration;
        private readonly IMongoClient _mongoClient;

        public MongoDbContext(IMongoDbConfiguration mongoDatabaseConfiguration)
        {
            _mongoDatabaseConfiguration = mongoDatabaseConfiguration;
            _mongoClient = new MongoClient(mongoDatabaseConfiguration.ConnectionString);
        }

        /// <summary>
        /// Method to get Mongo database
        /// </summary>
        /// <returns>Mongo database</returns>
        /// <exception cref="MongoConfigurationException"></exception>
        private IMongoDatabase GetMongoDatabase()
            => _mongoClient
                .GetDatabase(_mongoDatabaseConfiguration.DatabaseName) is IMongoDatabase mongoDatabase
                    ? mongoDatabase
                    : throw new MongoConfigurationException(nameof(_mongoDatabaseConfiguration));

        /// <summary>
        /// Method to get Mongo collection
        /// </summary>
        /// <returns>Mongo collection</returns>
        public IMongoCollection<T> GetMongoCollection()
            => GetMongoDatabase()
                .GetCollection<T>(typeof(T).Name);
    }
}
