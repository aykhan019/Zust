using System.Linq.Expressions;
using Zust.Core.Abstraction;

namespace Zust.Core.Concrete
{
    /// <summary>
    /// Represents a generic interface for a repository that handles entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Asynchronously retrieves a single entity based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter expression to apply for entity retrieval.</param>
        /// <returns>A task that represents the asynchronous operation with the retrieved entity, or null if not found.</returns>
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Asynchronously retrieves all entities based on the provided filter, if any.
        /// </summary>
        /// <param name="filter">The optional filter expression to apply for entity retrieval.</param>
        /// <returns>A task that represents the asynchronous operation with the collection of retrieved entities.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Asynchronously deletes an existing entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Asynchronously updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity);
    }
}