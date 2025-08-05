using Application.Abstractions;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class UserService : CrudService<UserEntity>, IUserService
    {
        IRepository<UserEntity> _repository;
        public UserService(IRepository<UserEntity> repository) : base(repository) 
        {
            this._repository = repository;
        }

        public Task<ICollection<UserEntity>> GetByEmailAsync(string email, CancellationToken ct) => _repository.FindAsync(u => u.Email == email, ct);

        public Task<ICollection<UserEntity>> GetByNameAsync(string name, CancellationToken ct) => _repository.FindAsync(u => u.Name == name, ct);
    }
}
