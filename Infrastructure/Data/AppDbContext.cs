using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;            
using Microsoft.Extensions.Configuration.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}