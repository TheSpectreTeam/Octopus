namespace Repository.MongoDb.Abstractions
{
    public interface IMongoRepository<T> : IRepositoryBase<T> where T : MongoEntityBase
    {
        IQueryable<T> AsQueryable();
        Task<T> GetOneAsync(Expression<Func<T, bool>> filterExpression);
        Task CreateManyAsync(IEnumerable<T> documents);
        Task<T> ReplaceOneAsync(T document);
        Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression);
    }
}
