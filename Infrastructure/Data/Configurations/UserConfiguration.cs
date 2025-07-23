using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
       public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasAlternateKey(u => u.Email);
            builder
                .HasMany(u => u.Cars)
                .WithOne(c => c.User);
        }
    }
}
