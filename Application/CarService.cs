
using System.Linq.Expressions;
using Application.Abstractions;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class CarService : CrudService<CarEntity>, ICarService
    {
        IRepository<CarEntity> _repository;
        public CarService(IRepository<CarEntity> repository) : base(repository)
        {
            this._repository = repository;
        }

        public async Task<ICollection<ReturnCarDto>?> GetAllPublicCarsAsync(CancellationToken ct)
        {
            var allCars = await _repository.GetAllAsync(ct);
            return allCars!
                .Select(c => new ReturnCarDto
                (
                    c.Id,
                    c.Mark,
                    c.Price,
                    c.Model,
                    c.Description,
                    new ReturnUserDto
                    (
                        c.User.Name,
                        c.User.Bio
                    )
                ))
                .ToList();
        }

        public async Task<ReturnCarDto?> GetPublicCarAsync(uint id, CancellationToken ct)
        {
            var car = await _repository.GetByIdAsync(id, ct);
            if(car is null) { return null; }
            return new ReturnCarDto(car.Id, car.Mark, car.Price, car.Model, car.Description, new ReturnUserDto(car.User.Name, car.User.Bio));
        }

        public async Task<ICollection<ReturnCarDto>?> GetByFiltersAsync(Expression<Func<CarEntity, bool>> predicate, CancellationToken ct)
        {
            var cars = await _repository.FindAsync(predicate, ct);

            return cars
                .Select(c => new ReturnCarDto
                (
                    c.Id,
                    c.Mark, 
                    c.Price,
                    c.Model,
                    c.Description,
                    new ReturnUserDto
                    (
                        c.User.Name,
                        c.User.Bio
                    )
                ))
                .ToList();
        }
    }
}
