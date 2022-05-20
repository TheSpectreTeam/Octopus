namespace Repository.Base.Abstractions
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        Task<object> CreateAsync(T entity);
        Task<bool> DeleteByIdAsync(object id);
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
