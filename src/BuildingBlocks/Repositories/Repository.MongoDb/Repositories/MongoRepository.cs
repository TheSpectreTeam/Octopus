namespace Repository.MongoDb.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IMongoEntityBase
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDbContext<T> mongoDbContext)
        {
            _collection = mongoDbContext.GetMongoCollection();
        }

        public IQueryable<T> AsQueryable()
            => _collection.AsQueryable();

        public async Task CreateAsync(T entity)
        {
            Guard.Against.Null(entity, nameof(entity));
            await _collection.InsertOneAsync(entity);
        }

        public async Task CreateManyAsync(IEnumerable<T> entities)
        {
            Guard.Against.Null(entities, nameof(entities));
            await _collection.InsertManyAsync(entities);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.IsObjectId(id, nameof(id));
            var filter = Builders<T>.Filter.Eq(item => item.Id, id as string);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            Guard.Against.Null(filterExpression, nameof(filterExpression));
            return await _collection.Find(filterExpression).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task<T> ReplaceOneAsync(T entity)
        {
            Guard.Against.Null(entity, nameof(entity));
            var filter = Builders<T>.Filter.Eq(item => item.Id, entity.Id);
            return await _collection.FindOneAndReplaceAsync(filter, entity);
        }

        public async Task DeleteByIdAsync(object id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.IsObjectId(id, nameof(id));
            var filter = Builders<T>.Filter.Eq(item => item.Id, id as string);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            Guard.Against.Null(filterExpression, nameof(filterExpression));
            await _collection.DeleteOneAsync(filterExpression);
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            Guard.Against.Null(filterExpression, nameof(filterExpression));
            await _collection.DeleteManyAsync(filterExpression);
        }
    }
}