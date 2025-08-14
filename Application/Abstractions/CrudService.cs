using Application.Interfaces;
using Core.Interfaces;
using AutoMapper;

namespace Application.Abstractions
{
    public abstract class CrudService<TEntity, TOutEntity> : ICrudService<TEntity, TOutEntity> 
        where TEntity : class, IDbEntity
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        
        protected CrudService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public virtual Task CreateAsync(TEntity entity, CancellationToken ct) => _repository.CreateAsync(entity, ct);
        public virtual async  Task<TOutEntity?> GetByIdAsync (uint id, CancellationToken ct)
        {
            var car = await _repository.GetByIdAsync(id, ct);
            return _mapper.Map<TOutEntity>(car);
        }

        public virtual Task UpdateAsync(TEntity entity, CancellationToken ct) => _repository.UpdateAsync(entity, ct);

        public virtual async Task<bool> DeleteAsync(uint id, CancellationToken ct)
        {
            var car = await _repository.GetByIdAsync(id, ct);
            if (car is null)
            {
                return false;
            }

            await _repository.DeleteAsync(car, ct);
            return true;
        }
    }
}
