namespace Repository.MongoDb.Abstractions
{
    public interface IMongoRepository<T> : IBaseRepository<T> where T : IMongoEntityBase
    {
        IQueryable<T> AsQueryable(AggregateOptions? aggregateOptions);
        Task<object> CreateAsync(T entity, InsertOneOptions? options, CancellationToken cancellationToken);
        Task<IDictionary<int, object>> CreateManyAsync(IEnumerable<T> entities, InsertManyOptions? options, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(object id, FindOptions? options);
        Task<T> GetOneAsync(Expression<Func<T, bool>> filterExpression, FindOptions? options);
        Task<IEnumerable<T>> GetAllAsync(FindOptions? options = null);
        Task<T> ReplaceOneAsync(T entity, FindOneAndReplaceOptions<T, T>? options, CancellationToken cancellationToken);
        Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken);
        Task<bool> DeleteOneAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken);
        Task<long> DeleteManyAsync(Expression<Func<T, bool>> filterExpression, DeleteOptions? options, CancellationToken cancellationToken);
    }
}