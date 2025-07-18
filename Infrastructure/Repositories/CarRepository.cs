using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class CarRepository(AppDbContext context) : ICarRepository
    {
        public async Task CreateAsync(CarEntity car, CancellationToken ct = default)
        {
            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarEntity>?> GetAllAsync(CancellationToken ct)
        {
            return context.Cars.OrderBy(c => c.Id);
        }

        public async Task<CarEntity?> GetByIdAsync(uint id, CancellationToken ct)
        {
            return await context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task UpdateAsync(CarEntity car, CancellationToken ct)
        {
            context.Cars.Update(car);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(CarEntity car, CancellationToken ct = default)
        {
            context.Cars.Remove(car);
            await context.SaveChangesAsync();
        }
    }
}
