using Core.Entities;
using Core.Interfaces;

namespace Application.Interfaces
{
    public interface ICrudService<TEntity, TOutEntity>  where TEntity : class, IDbEntity
    {
        Task<OperationResult<TOutEntity?>> GetByIdAsync(uint id, CancellationToken ct);
        Task<OperationResult<uint>> CreateAsync(TEntity entity, CancellationToken ct);
        Task<OperationResult<uint>> DeleteAsync(uint id, uint requestId, CancellationToken ct);
    }
}
