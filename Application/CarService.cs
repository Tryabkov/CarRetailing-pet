using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class CarService : CrudService<CarEntity>, ICarService
    {
        IRepository<CarEntity> repository;
        public CarService(IRepository<CarEntity> repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<ICollection<ReturnCarDto>?> GetAllPublicCarsAsync(CancellationToken ct)
        {
            var allCars = await repository.GetAllAsync(ct);
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
            var car = await repository.GetByIdAsync(id, ct);
            if(car is null) { return null; }
            return new ReturnCarDto(car.Id, car.Mark, car.Price, car.Model, car.Description, new ReturnUserDto(car.User.Name, car.User.Bio));
        }

        public async Task<ICollection<ReturnCarDto>?> GetByFiltersAsync(Expression<Func<CarEntity, bool>> predicate, CancellationToken ct)
        {
            var cars = await repository.FindAsync(predicate, ct);

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
