using Common.Models.DomainModels;

namespace Repository.Base.Abstractions
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task CreateAsync(T item);
        Task RemoveAsync(int id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
