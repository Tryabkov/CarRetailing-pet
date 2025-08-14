using Application.Abstractions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class CarService : CrudService<CarEntity, ReturnCarDto>,  ICarService
    {
        public CarService(IRepository<CarEntity> repository, IMapper mapper)
            : base(repository, mapper) { }
        public async Task<List<ReturnCarDto>> GetByFilterAsync(CarFilters filters, CancellationToken ct) => 
            await ApplyParameters(_repository.Query(ct), filters)
                .Include(car => car.User)
                .ProjectTo<ReturnCarDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

        public override async Task<ReturnCarDto?> GetByIdAsync(uint id, CancellationToken ct)
        {
            var car = await _repository.GetByIdAsync(id, ct);
            if (car is null) { return null; }
            return new ReturnCarDto
            (
                car.Id,
                car.Mark,
                car.Model,
                car.Price,
                car.Year,
                car.Mileage,
                car.Description,
                new ReturnUserDto(car.User.Name, car.User.PhoneNumber, car.User.Bio)
            );
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
            
            int page = filters.Page ?? 1;
            int pageSize = filters.PageSize ?? 10;
            
            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
