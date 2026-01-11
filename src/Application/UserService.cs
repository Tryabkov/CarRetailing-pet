using Application.Abstractions;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class UserService : CrudService<UserEntity, ReturnUserDto>, IUserService
{
    public UserService(IRepository<UserEntity> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }

    public Task<List<UserEntity>> GetByEmailAsync(string email, CancellationToken ct)
    {
        return _repository
            .Query(ct)
            .Where(u => u.Email == email)
            .ToListAsync(ct);
    }

    public Task<List<UserEntity>> GetByNameAsync(string name, CancellationToken ct)
    {
        return _repository
            .Query(ct)
            .Where(u => u.Name == name)
            .ToListAsync(ct);
    }

    protected override bool VerifyId(uint requestId, UserEntity entity, CancellationToken ct)
    {
        return entity.Id == requestId;
    }
}