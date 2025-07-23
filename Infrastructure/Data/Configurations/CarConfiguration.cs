using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class CarConfiguration : IEntityTypeConfiguration<CarEntity>
    {
        public void Configure(EntityTypeBuilder<CarEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder
                .HasOne(c => c.User)
                .WithMany(u => u.Cars)
                .HasForeignKey(c => c.UserId);
        }
    }
}
