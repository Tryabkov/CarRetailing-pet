using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class CarRepository : GenericRepository<CarEntity>
    {
        public CarRepository(AppDbContext context) : base(context) { }

        public override async Task<ICollection<CarEntity>> GetAllAsync(CancellationToken ct)
        {
            return await _dbSet
                .Include(c => c.User)
                .OrderBy(с => с.Id)
                .ToListAsync(ct);
        }

        public override async Task<CarEntity?> GetByIdAsync(uint id, CancellationToken ct)
        {
            return await _dbSet
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id, ct);
                
        }


    }
}
