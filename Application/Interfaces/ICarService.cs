using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarService : ICrudService<CarEntity>
    {
        Task<ICollection<CarEntity>?> GetByFiltersAsync(CancellationToken ct);
        Task<ICollection<ReturnCarDto>?> GetAllPublicCarsAsync(CancellationToken ct);
        Task<ReturnCarDto?> GetPublicCarAsync(uint id, CancellationToken ct);
    }
}
