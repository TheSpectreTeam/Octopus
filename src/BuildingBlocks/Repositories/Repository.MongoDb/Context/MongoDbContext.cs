namespace Repository.MongoDb.Context
{
    public class MongoDbContext<T> : IMongoDbContext<T> where T : IMongoEntityBase
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoDbConfiguration mongoDatabaseConfiguration)
        {
            var client = new MongoClient(mongoDatabaseConfiguration.ConnectionString);
            _database = client.GetDatabase(mongoDatabaseConfiguration.DatabaseName);
        }

        public IMongoCollection<T> GetMongoCollection()
        => _database
            .GetCollection<T>(typeof(T).Name);
    }
}
