using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class UserService : CrudService<UserEntity>, IUserService
    {
        IRepository<UserEntity> repository;
        public UserService(IRepository<UserEntity> repository) : base(repository) 
        {
            this.repository = repository;
        }

        public Task<ICollection<UserEntity>> GetByEmailAsync(string email, CancellationToken ct) => repository.FindAsync(u => u.Email == email, ct);

        public Task<ICollection<UserEntity>> GetByNameAsync(string name, CancellationToken ct) => repository.FindAsync(u => u.Name == name, ct);
    }
}
