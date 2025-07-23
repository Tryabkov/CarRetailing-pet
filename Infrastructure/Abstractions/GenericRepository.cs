using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity: class, IDbEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task CreateAsync(TEntity entity, CancellationToken ct)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken ct)
        {
            return await _dbSet.OrderBy(e => e.Id).ToListAsync(ct);
        }

        public virtual async Task<TEntity?> GetByIdAsync(uint id, CancellationToken ct)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await _dbSet.Where(predicate).ToListAsync(ct);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
