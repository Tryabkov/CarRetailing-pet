using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class UserRepository : GenericRepository<UserEntity>
    {
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
