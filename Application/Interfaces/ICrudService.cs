using Core.Interfaces;

namespace Application.Interfaces
{
    public interface ICrudService<TEntity> where TEntity : class, IDbEntity
    {
        Task<ICollection<TEntity>?> GetAllAsync(CancellationToken ct);
        Task<TEntity?> GetByIdAsync(uint id, CancellationToken ct);
        Task CreateAsync(TEntity entity, CancellationToken ct);
        Task UpdateAsync(TEntity entity, CancellationToken ct);
        Task<bool> DeleteAsync(uint id, CancellationToken ct);
    }
}
