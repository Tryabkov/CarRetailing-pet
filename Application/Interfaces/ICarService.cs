﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarService : ICrudService<CarEntity>
    {
        Task<ICollection<CarEntity>?> GetByFiltersAsync();
    }
}
