using Core.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    internal class UserRepository(AppDbContext context) : GenericRepository<UserEntity>(context);
}
