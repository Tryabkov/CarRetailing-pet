using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarService : ICrudService<CarEntity, ReturnCarDto>
    {
        Task<List<ReturnCarDto>> GetByFilterAsync(CarFilters filters, CancellationToken ct);
    }
}
