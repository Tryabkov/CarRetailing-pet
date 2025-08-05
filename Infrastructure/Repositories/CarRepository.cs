using Core.Entities;
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
            return await DbSet
                .Include(c => c.User)
                .OrderBy(с => с.Id)
                .ToListAsync(ct);
        }

        public override async Task<CarEntity?> GetByIdAsync(uint id, CancellationToken ct)
        {
            return await DbSet
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id, ct);    
        }
    }
}
