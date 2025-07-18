using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarEntity>?> GetAllAsync(CancellationToken ct);
        Task<CarEntity?> GetByIdAsync(uint id, CancellationToken ct);
        Task CreateAsync(CarEntity car, CancellationToken ct);
        Task UpdateAsync(CarEntity car, CancellationToken ct);
        Task DeleteAsync(uint id, CancellationToken ct);

    }
}
