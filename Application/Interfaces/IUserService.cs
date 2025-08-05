using Core.Entities;

namespace Application.Interfaces
{
    public interface IUserService : ICrudService<UserEntity>
    {
        Task<ICollection<UserEntity>> GetByNameAsync(string name, CancellationToken ct);
        Task<ICollection<UserEntity>> GetByEmailAsync(string email, CancellationToken ct);
    }
}
