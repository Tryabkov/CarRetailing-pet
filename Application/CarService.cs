using Application.Abstractions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class CarService : CrudService<CarEntity, ReturnCarDto>, ICarService
{
    public event EventHandler<CarEntity>? OnCarCreated;
    public event EventHandler<CarEntity>? OnCarUpdated;
    public event EventHandler<CarFilters>? OnFilterSearch;
    public event EventHandler<uint>? OnCarClicked;
    
    private readonly IEventBus<CarEvent> _carEventBus;
    private readonly IEventBus<SearchEvent> _filtersEventBus;
    public CarService(IRepository<CarEntity> repository, 
        IMapper mapper, 
        IEventBus<CarEvent> carEventBus,
        IEventBus<SearchEvent> filtersEventBus)
        : base(repository, mapper)
    {
        _carEventBus = carEventBus;
        _filtersEventBus = filtersEventBus;
    }

    public Task<OperationResult<ReturnCarDto?>> GetByIdAsync(uint id, CancellationToken ct)
    {
        OnCarClicked?.Invoke(this, id);
        return base.GetByIdAsync(id, ct);
    }

    public async Task<OperationResult<List<ReturnCarDto>>> GetByFilterAsync(CarFilters filters,
        CancellationToken ct)
    {
        try
        {
            var result = await ApplyParameters(_repository.Query(ct), filters)
                .Include(car => car.User)
                .ProjectTo<ReturnCarDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
            await _filtersEventBus.PublishAsync(new SearchEvent("search", filters), ct);
            // OnFilterSearch?.Invoke(this, filters);
            return OperationResult<List<ReturnCarDto>>.Success(result);
        }
        catch (Exception e)
        {
            return OperationResult<List<ReturnCarDto>>.ServerError("Get failed");
        }
    }

    public override async Task<OperationResult<uint>> CreateAsync(CarEntity car, CancellationToken ct)
    {
        try
        {
            var id = await _repository.CreateAsync(car, ct);
            await _carEventBus.PublishAsync(new CarEvent("created", car), ct);
            // OnCarCreated?.Invoke(this, car);
            return new OperationResult<uint>(OperationResultType.Success, id);
        }
        catch (Exception e)
        {
            return OperationResult<uint>.ServerError("Creation failed");
        }
    }

    public async Task<OperationResult<uint>> UpdateAsync(uint id, uint requestId, UpdateCarDto updateCar,
        CancellationToken ct)
    {
        try
        {
            var car = await _repository
                .Query(ct)
                .Where(car => car.Id == id)
                .Include(car => car.User)
                .FirstOrDefaultAsync(ct);

            if (car is null) return OperationResult<uint>.Forbidden();
            if (car.UserId != requestId) return OperationResult<uint>.NotFound();
            _mapper.Map(updateCar, car);
            await _repository.UpdateAsync(car, ct);
            await _carEventBus.PublishAsync(new CarEvent("updated", car), ct);
            return OperationResult<uint>.Success(requestId);
        }
        catch (Exception e)
        {
            return OperationResult<uint>.ServerError("Update failed");
        }
    }
    
    private static IQueryable<CarEntity> ApplyParameters(IQueryable<CarEntity> query, CarFilters filters)
    {
        if (!string.IsNullOrWhiteSpace(filters.Mark))
            query = query.Where(c => c.Mark == filters.Mark);

        if (!string.IsNullOrWhiteSpace(filters.Model))
            query = query.Where(c => c.Model == filters.Model);

        if (filters.MinPrice.HasValue)
            query = query.Where(c => c.Price >= filters.MinPrice.Value);

        if (filters.MaxPrice.HasValue)
            query = query.Where(c => c.Price <= filters.MaxPrice.Value);

        if (filters.Year.HasValue)
            query = query.Where(c => c.Year == filters.Year.Value);

        if (filters.MaxMileage.HasValue)
            query = query.Where(c => c.Mileage <= filters.MaxMileage.Value);

        query = query.OrderBy(c => c.Id);

        var page = filters.Page ?? 1;
        var pageSize = filters.PageSize ?? 10;

        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    protected override bool VerifyId(uint requestId, CarEntity entity, CancellationToken ct)
    {
        return entity.User.Id == requestId;
    }
}