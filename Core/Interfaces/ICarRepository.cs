using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICarRepository
    {
        Task CreateAsync(CarEntity car, CancellationToken ct);
        Task<IEnumerable<CarEntity>?> GetAllAsync(CancellationToken ct);
        Task<CarEntity?> GetByIdAsync(uint id, CancellationToken ct);
        Task UpdateAsync(CarEntity car, CancellationToken ct);
        Task DeleteAsync(CarEntity car, CancellationToken ct);

    }
}
