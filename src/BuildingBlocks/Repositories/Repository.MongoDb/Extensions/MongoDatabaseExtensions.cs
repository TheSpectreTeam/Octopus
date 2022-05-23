namespace Repository.MongoDb.Extensions
{
    public static class MongoDatabaseExtensions
    {
        /// <summary>
        /// Mongo database connectivity extension.
        /// </summary>
        /// <param name="mongoDatabase"></param>
        /// <returns>Connecting is possible or not.</returns>
        public static bool IsConnectionSuccess(this IMongoDatabase mongoDatabase) =>
            mongoDatabase
                .RunCommandAsync(
                    command: (Command<BsonDocument>)"{ping:1}")
                .Wait(1000);
    }
}