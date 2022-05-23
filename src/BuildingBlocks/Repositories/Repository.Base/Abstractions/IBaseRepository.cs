namespace Repository.Base.Abstractions
{
    /// <summary>
    /// Base repository interface providing basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        /// <summary>
        /// Creates a single entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The entity id.</returns>
        Task<object> CreateAsync(T entity);

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>Successfully deleted or not.</returns>
        Task<bool> DeleteByIdAsync(object id);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>The entity.</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>The entities.</returns>
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
