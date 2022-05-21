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

        private IMongoDatabase GetMongoDatabase(MongoDatabaseSettings? databaseSettings = null)
        {
            var mongoDatabase = _mongoClient.GetDatabase(
                name: _mongoDatabaseConfiguration.DatabaseName,
                settings: databaseSettings);

            return mongoDatabase.IsConnectionSuccess()
                ? mongoDatabase
                : throw new MongoConfigurationException(nameof(_mongoDatabaseConfiguration));
        }

        public IMongoCollection<T> GetMongoCollection(
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null)
            => GetMongoDatabase(databaseSettings)
                .GetCollection<T>(
                name: typeof(T).Name,
                settings: collectionSettings);
    }
}
