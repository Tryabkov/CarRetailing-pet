using System.Linq.Expressions;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions
{
    public abstract class GenericRepository<TEntity>(DbContext context) : IRepository<TEntity>
        where TEntity : class, IDbEntity
    {
        protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

        public virtual async Task CreateAsync(TEntity entity, CancellationToken ct)
        {
            await DbSet.AddAsync(entity, ct);
            await context.SaveChangesAsync(ct);
        }

        public Task<TEntity?> GetByIdAsync(uint id, CancellationToken ct) =>
            DbSet.Where(e => e.Id == id).FirstOrDefaultAsync(ct);

        public virtual IQueryable<TEntity> Query(CancellationToken ct) => DbSet;

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct)
        {
            DbSet.Update(entity);
            await context.SaveChangesAsync(ct);
        }
        
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct)
        {
            DbSet.Remove(entity);
            await context.SaveChangesAsync(ct);
        }
    }
}
