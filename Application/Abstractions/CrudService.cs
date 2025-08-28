using Application.Interfaces;
using Core.Interfaces;
using AutoMapper;
using Core.Entities;

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
        
        public virtual async Task<OperationResult<uint>> CreateAsync(TEntity entity, CancellationToken ct)
        {
            try
            {
                uint id = await _repository.CreateAsync(entity, ct);
                return new OperationResult<uint>(OperationResultType.Success, id);
            }
            catch (Exception e)
            {
                return OperationResult<uint>.ServerError("Creation failed");
            }
        } 
        
        public virtual async  Task<OperationResult<TOutEntity?>> GetByIdAsync (uint id, CancellationToken ct)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id, ct);
                return entity == null ? 
                    OperationResult<TOutEntity?>.NotFound() : OperationResult<TOutEntity?>.Success(_mapper.Map<TOutEntity>(entity));
            }
            catch (Exception e)
            {
                return OperationResult<TOutEntity?>.ServerError("Get failed");
            }
        }
        
        public virtual async Task<OperationResult<uint>> DeleteAsync(uint id, uint requestId, CancellationToken ct)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id, ct);
                if (entity is null) return OperationResult<uint>.NotFound();

                if (!VerifyId(requestId, entity, ct)) return OperationResult<uint>.Forbidden();
                    
                await _repository.DeleteAsync(entity, ct);
                return new OperationResult<uint>(OperationResultType.Success, id);
            }
            catch (Exception e)
            {
                return OperationResult<uint>.ServerError("Delete failed");
            }
        }

        protected abstract bool VerifyId(uint requestId, TEntity entity, CancellationToken ct);
    }
}