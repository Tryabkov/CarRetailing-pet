using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class CarService : CrudService<CarEntity>, ICarService
    {
        IRepository<CarEntity> repository;
        public CarService(IRepository<CarEntity> repository) : base(repository)
        {
            this.repository = repository;
        }
        public Task<ICollection<CarEntity>?> GetByFiltersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
