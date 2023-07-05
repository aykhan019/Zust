using System.Linq.Expressions;
using Zust.Core.Abstraction;

namespace Zust.Core.Concrete
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
