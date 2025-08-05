using Core.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    internal class UserRepository : GenericRepository<UserEntity>
    {
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
