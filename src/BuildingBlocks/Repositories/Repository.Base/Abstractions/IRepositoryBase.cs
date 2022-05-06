namespace Repository.Base.Abstractions
{
    public interface IRepositoryBase<T> where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteByIdAsync(object id);
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
