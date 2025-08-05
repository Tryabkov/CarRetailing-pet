using System.Linq.Expressions;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity: class, IDbEntity
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task CreateAsync(TEntity entity, CancellationToken ct)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken ct)
        {
            return await DbSet.OrderBy(e => e.Id).ToListAsync(ct);
        }

        public virtual async Task<TEntity?> GetByIdAsync(uint id, CancellationToken ct)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await DbSet.Where(predicate).ToListAsync(ct);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
