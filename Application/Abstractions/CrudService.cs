using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Abstractions
{
    public abstract class CrudService<TEntity>(IRepository<TEntity> repository) : ICrudService<TEntity> 
        where TEntity : class, IDbEntity
    {
        public virtual Task CreateAsync(TEntity entity, CancellationToken ct) => repository.CreateAsync(entity, ct);
        public virtual Task<ICollection<TEntity>?> GetAllAsync(CancellationToken ct) => repository.GetAllAsync(ct);
        public virtual Task<TEntity?> GetByIdAsync(uint id, CancellationToken ct) => repository.GetByIdAsync(id, ct);
        public virtual Task UpdateAsync(TEntity entity, CancellationToken ct) => repository.UpdateAsync(entity, ct);

        public virtual async Task<bool> DeleteAsync(uint id, CancellationToken ct)
        {
            var car = await repository.GetByIdAsync(id, ct);
            if (car is null)
            {
                return false;
            }

            await repository.DeleteAsync(car, ct);
            return true;
        }
    }
}
