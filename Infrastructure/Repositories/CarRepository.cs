using Core.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class CarRepository(AppDbContext context) : GenericRepository<CarEntity>(context);
}
