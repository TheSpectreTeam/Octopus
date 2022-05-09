namespace Repository.MongoDb.Extensions
{
    public static class MongoDatabaseExtensions
    {
        public static bool IsConnectionSuccess(this IMongoDatabase mongoDatabase) => 
            mongoDatabase
                .RunCommandAsync((Command<BsonDocument>)"{ping:1}")
                .Wait(1000);
    }
}
