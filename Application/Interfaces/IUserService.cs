using Core.Entities;

namespace Application.Interfaces
{
    public interface IUserService : ICrudService<UserEntity, ReturnUserDto>
    {
        Task<List<UserEntity>> GetByNameAsync(string name, CancellationToken ct);
        Task<List<UserEntity>> GetByEmailAsync(string email, CancellationToken ct);
    }
}
