namespace Repository.MongoDb.Abstractions
{
    public interface IMongoDbContext<T> where T : IMongoEntityBase
    {
        /// <summary>
        /// Returns a Mongo collection.
        /// </summary>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <returns>The mongo collection.</returns>
        IMongoCollection<T> GetMongoCollection(
            MongoDatabaseSettings? databaseSettings = null, 
            MongoCollectionSettings? collectionSettings = null);
    }
}
