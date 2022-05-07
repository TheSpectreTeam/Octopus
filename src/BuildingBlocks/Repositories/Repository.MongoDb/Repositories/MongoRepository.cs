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
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));
            await _collection
                .InsertOneAsync(entity);
        }

        public async Task CreateManyAsync(IEnumerable<T> entities)
        {
            if (_collection == null) 
                throw new ArgumentNullException(nameof(entities));
            await _collection
                .InsertManyAsync(entities);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id is ObjectId i)
            {
                var fiter = Builders<T>.Filter.Eq(item => item.Id.Equals(i), true);
                return await _collection.Find(fiter).SingleOrDefaultAsync();
            }
            throw new ArgumentException(nameof(id));
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> filterExpression)
            => await _collection
                .Find(filterExpression).SingleOrDefaultAsync();

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _collection
                .Find(_ => true).ToListAsync();

        public async Task<T> ReplaceOneAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var fiter = Builders<T>.Filter.Eq(item => item.Id.Equals(entity.Id), true);
            return await _collection
                .FindOneAndReplaceAsync(fiter, entity);
        }

        public async Task DeleteByIdAsync(object id)
        {
            if (id == null) 
                throw new ArgumentNullException(nameof(id));
            if (id is ObjectId i)
            {
                var fiter = Builders<T>.Filter.Eq(item => item.Id.Equals(i), true);
                await _collection
                    .DeleteOneAsync(fiter);
            }
            else throw new ArgumentException(nameof(id));
        }

        public async Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            if (filterExpression == null) 
                throw new ArgumentNullException(nameof(filterExpression));
            await _collection
                .DeleteOneAsync(filterExpression);
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            if (filterExpression == null) 
                throw new ArgumentNullException(nameof(filterExpression));
            await _collection
                .DeleteManyAsync(filterExpression);
        } 
    }
}
