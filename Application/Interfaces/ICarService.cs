using System.Linq.Expressions;
using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarService : ICrudService<CarEntity>
    {
        Task<ICollection<ReturnCarDto>?> GetByFiltersAsync(Expression<Func<CarEntity, bool>> predicate, CancellationToken ct);
        Task<ICollection<ReturnCarDto>?> GetAllPublicCarsAsync(CancellationToken ct);
        Task<ReturnCarDto?> GetPublicCarAsync(uint id, CancellationToken ct);
    }
}
