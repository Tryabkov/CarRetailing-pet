using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task CreateAsync(TEntity entity, CancellationToken ct);
        Task<ICollection<TEntity>?> GetAllAsync(CancellationToken ct);
        Task<TEntity> GetByIdAsync(uint id, CancellationToken ct);
        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
        Task UpdateAsync(TEntity entity, CancellationToken ct);
        Task DeleteAsync(TEntity entity, CancellationToken ct);

    }
}
