using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Core.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>
        : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }   
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FirstOrDefaultAsync(filter);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (var context = new TContext())
            {
                var items = context.Set<TEntity>();
                return filter == null ? await items.ToListAsync() : await items.Where(filter).ToListAsync();
            }
        }
    }
}
