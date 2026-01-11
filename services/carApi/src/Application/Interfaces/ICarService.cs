using Core.Entities;

namespace Application.Interfaces;

public interface ICarService : ICrudService<CarEntity, ReturnCarDto>
{
    event EventHandler<CarEntity>? OnCarCreated;
    event EventHandler<CarEntity>? OnCarUpdated;
    event EventHandler<CarFilters>? OnFilterSearch;
    
    
    Task<OperationResult<List<ReturnCarDto>>> GetByFilterAsync(CarFilters filters, CancellationToken ct);
    Task<OperationResult<uint>> UpdateAsync(uint id, uint requestId, UpdateCarDto dto, CancellationToken ct);
}