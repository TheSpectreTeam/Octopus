namespace Repository.MongoDb.Abstractions
{
    public interface IMongoRepository<T> : IBaseRepository<T> where T : IMongoEntityBase
    {
        /// <summary>
        /// Creates a queryable source of documents.
        /// </summary>
        /// <param name="aggregateOptions">The options for an aggregate operation.</param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <returns>A queryable source of documents.</returns>
        IQueryable<T> AsQueryable(
            AggregateOptions? aggregateOptions = null,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null);

        /// <summary>
        /// Creates a single entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="options">The options for inserting one document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The entity id.</returns>
        Task<object> CreateAsync(
            T entity,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            InsertOneOptions? options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts many entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="options">The ptions for inserting many documents.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Dictionary with pairs of values ​​entity serial number - its id.</returns>
        Task<IDictionary<int, object>> CreateManyAsync(
            IEnumerable<T> entities,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            InsertManyOptions? options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="options">The options for finding documents.</param>
        /// <returns>The entity.</returns>
        Task<T> GetByIdAsync(
            object id,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            FindOptions? options = null);

        /// <summary>
        /// Get entity by filter expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="options">The options for finding documents.</param>
        /// <returns>The entity.</returns>
        Task<T> GetOneAsync(
            Expression<Func<T, bool>> filterExpression,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            FindOptions? options = null);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="options">The options for finding documents.</param>
        /// <returns>The entities.</returns>
        Task<IReadOnlyList<T>> GetAllAsync(
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            FindOptions? options = null);

        /// <summary>
        /// Replace one entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="options">The options for a findAndModify command to replace an object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The entity.</returns>
        Task<T> ReplaceOneAsync(
            T entity,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            FindOneAndReplaceOptions<T, T>? options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Successfully deleted or not.</returns>
        Task<bool> DeleteByIdAsync(
            object id,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entity by filter expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>Successfully deleted or not.</returns>
        Task<bool> DeleteOneAsync(
            Expression<Func<T, bool>> filterExpression,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete many entities by filter expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="databaseSettings">The settings used to access a database.</param>
        /// <param name="collectionSettings">The settings used to access a collection.</param>
        /// <param name="options">The options for the Delete methods.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The number of successfully deleted entities.</returns>
        Task<long> DeleteManyAsync(
            Expression<Func<T, bool>> filterExpression,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            DeleteOptions? options = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}