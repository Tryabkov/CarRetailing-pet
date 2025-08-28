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

        public async Task<OperationResult<List<ReturnCarDto>>> GetByFilterAsync(CarFilters filters,
            CancellationToken ct)
        {
            try
            {
                var result = await ApplyParameters(_repository.Query(ct), filters)
                    .Include(car => car.User)
                    .ProjectTo<ReturnCarDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(ct);
                return OperationResult<List<ReturnCarDto>>.Success(result);
            }
            catch (Exception e)
            {
                return OperationResult<List<ReturnCarDto>>.ServerError("Get failed");
            }
           
        }

        public async Task<OperationResult<uint>> UpdateAsync(uint id, uint requestId, UpdateCarDto updateCar, CancellationToken ct)
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
            
            int page = filters.Page ?? 1;
            int pageSize = filters.PageSize ?? 10;
            
            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        protected override bool VerifyId(uint requestId, CarEntity entity, CancellationToken ct)
            => entity.User.Id == requestId;
    }
}
