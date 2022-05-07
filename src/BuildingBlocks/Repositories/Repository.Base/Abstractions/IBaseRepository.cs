namespace Repository.Base.Abstractions
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        Task CreateAsync(T entity);
        Task DeleteByIdAsync(object id);
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
