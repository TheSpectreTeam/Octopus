namespace Repository.MongoDb.Extensions
{
    public static class MongoDatabaseExtensions
    {
        /// <summary>
        /// Mongo Database Connectivity Extension
        /// </summary>
        /// <param name="mongoDatabase"></param>
        /// <param name="readPreference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Connecting is possible or not</returns>
        public static bool IsConnectionSuccess(
            this IMongoDatabase mongoDatabase, 
            ReadPreference? readPreference = null, 
            CancellationToken cancellationToken = default(CancellationToken)) 
            => mongoDatabase
                .RunCommandAsync(
                    command: (Command<BsonDocument>)"{ping:1}",
                    readPreference: readPreference,
                    cancellationToken: cancellationToken)
                .Wait(1000);
    }
}
