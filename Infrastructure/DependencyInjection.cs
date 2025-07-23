using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)  
        {
            var connString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connString));
            services.AddScoped<IRepository<CarEntity>, CarRepository>();
            services.AddScoped<IRepository<UserEntity>, UserRepository>();

            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            
            return services;
        }

        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var JwtSettings = configuration.GetSection("Jwt");
            byte[] key = Encoding.UTF8.GetBytes(JwtSettings["Key"]!);
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = true;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer =           false,
                    ValidateAudience =         false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =         new SymmetricSecurityKey(key),
                    ClockSkew =                TimeSpan.Zero
                };
            });
            services.AddAuthorization();

            return services;
        }
    }
}
