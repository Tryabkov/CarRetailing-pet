using System.Linq.Expressions;

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
