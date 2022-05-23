namespace Repository.MongoDb.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IMongoEntityBase
    {
        private readonly IMongoDbContext<T> _mongoDbContext;

        public MongoRepository(IMongoDbContext<T> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public IQueryable<T> AsQueryable(
            AggregateOptions? aggregateOptions = null,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null) =>
            _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .AsQueryable(aggregateOptions);

        public async Task<object> CreateAsync(T entity)
        {
            Guard.Against.Null(entity, nameof(entity));
            await _mongoDbContext
                .GetMongoCollection()
                .InsertOneAsync(
                    document: entity);
            return entity.Id;
        }

        public async Task<object> CreateAsync(
            T entity,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            InsertOneOptions? options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.Against.Null(entity, nameof(entity));
            await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .InsertOneAsync(
                    document: entity,
                    options: options,
                    cancellationToken: cancellationToken);

            return entity.Id;
        }

        public async Task<IDictionary<int, object>> CreateManyAsync(
            IEnumerable<T> entities,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            InsertManyOptions? options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.Against.Null(entities, nameof(entities));
            await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .InsertManyAsync(
                    documents: entities,
                    options: options,
                    cancellationToken: cancellationToken);

            var idDictionary = new Dictionary<int, object>();
            for (int i = 0; i < entities.Count(); i++)
            {
                idDictionary.Add(i, entities.ElementAt(i).Id);
            }
            return idDictionary;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.InvalidObjectId(id, nameof(id));
            var filter = Builders<T>
                .Filter
                .Eq(
                    field: item => item.Id,
                    value: id as string);
            return await _mongoDbContext
                .GetMongoCollection()
                .Find(
                    filter: filter)
                .SingleOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(
            object id,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            FindOptions? options = null)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.InvalidObjectId(id, nameof(id));
            var filter = Builders<T>
                .Filter
                .Eq(
                    field: item => item.Id,
                    value: id as string);
            return await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .Find(
                    filter: filter,
                    options: options)
                .SingleOrDefaultAsync();
        }

        public async Task<T> GetOneAsync(
            Expression<Func<T, bool>> filterExpression,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            FindOptions? options = null)
        {
            Guard.Against.Null(filterExpression, nameof(filterExpression));
            return await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .Find(
                    filter: filterExpression,
                    options: options)
                .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _mongoDbContext
                .GetMongoCollection()
                .Find(
                    filter: _ => true)
                .ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            FindOptions? options = null)
            => await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .Find(
                    filter: _ => true,
                    options: options)
                .ToListAsync();

        public async Task<T> ReplaceOneAsync(
            T entity,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            FindOneAndReplaceOptions<T, T>? options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.Against.Null(entity, nameof(entity));
            var filter = Builders<T>
                .Filter
                .Eq(
                    field: item => item.Id,
                    value: entity.Id);
            return await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .FindOneAndReplaceAsync(
                    filter: filter,
                    replacement: entity,
                    options: options,
                    cancellationToken: cancellationToken);
        }

        public async Task<bool> DeleteByIdAsync(object id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.InvalidObjectId(id, nameof(id));
            var filter = Builders<T>
                .Filter
                .Eq(
                    field: item => item.Id,
                    value: id as string);
            var deleteResult = await _mongoDbContext
                .GetMongoCollection()
                .DeleteOneAsync(
                    filter: filter);
            return deleteResult
                .DeletedCount != 0;
        }

        public async Task<bool> DeleteByIdAsync(
            object id,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.InvalidObjectId(id, nameof(id));
            var filter = Builders<T>
                .Filter
                .Eq(
                    field: item => item.Id,
                    value: id as string);
            var deleteResult = await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .DeleteOneAsync(
                    filter: filter,
                    cancellationToken: cancellationToken);
            return deleteResult
                .DeletedCount != 0;
        }

        public async Task<bool> DeleteOneAsync(
            Expression<Func<T, bool>> filterExpression,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.Against.Null(filterExpression, nameof(filterExpression));
            var deleteResult = await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .DeleteOneAsync(
                    filter: filterExpression,
                    cancellationToken: cancellationToken);
            return deleteResult
                .DeletedCount != 0;
        }

        public async Task<long> DeleteManyAsync(
            Expression<Func<T, bool>> filterExpression,
            MongoDatabaseSettings? databaseSettings = null,
            MongoCollectionSettings? collectionSettings = null,
            DeleteOptions? options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.Against.Null(filterExpression, nameof(filterExpression));
            var deleteResult = await _mongoDbContext
                .GetMongoCollection(
                    databaseSettings: databaseSettings,
                    collectionSettings: collectionSettings)
                .DeleteManyAsync(
                    filter: filterExpression,
                    options: options,
                    cancellationToken: cancellationToken);

            return deleteResult.DeletedCount;
        }
    }
}