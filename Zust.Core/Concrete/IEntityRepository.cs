using System.Linq.Expressions;
using Zust.Core.Abstraction;

namespace Zust.Core.Concrete
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T?> Get(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null);

        Task Add(T entity);

        Task Delete(T entity);

        Task Update(T entity);
    }
}
