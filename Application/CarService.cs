using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class CarService(ICarRepository repository) : ICarService
    {
        public Task CreateAsync(CarEntity car, CancellationToken ct = default)
        {
            return repository.CreateAsync(car, ct);
        }

        public async Task<IEnumerable<CarEntity>?> GetAllAsync(CancellationToken ct)
        {
            return await repository.GetAllAsync(ct);
        }

        public async Task<CarEntity?> GetByIdAsync(uint id, CancellationToken ct)
        {
            var car = await repository.GetByIdAsync(id, ct);
            return car;
        }

        public Task UpdateAsync(CarEntity car, CancellationToken ct)
        {
            return repository.UpdateAsync(car, ct);
        }

        public async Task DeleteAsync(uint id, CancellationToken ct = default)
        {
            var car = await repository.GetByIdAsync(id, ct);
            if (car is null)
            {
                throw new Exception("Car not found");
            }

            await repository.DeleteAsync(car, ct);
        }
    }
}
