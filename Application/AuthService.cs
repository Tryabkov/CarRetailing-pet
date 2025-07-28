using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application
{
    public class AuthService(IConfiguration config, IUserService userService) : IAuthService
    {
        private readonly PasswordHasher<UserEntity> hasher = new();

        public async Task<LoginResult> LoginAsync(string email, string password, CancellationToken ct)
        {
            var users = await userService.GetByEmailAsync(email, ct);
            if (users.Count == 0) 
            {
                return new LoginResult(LoginResultType.UserNotFound); 
            }

            string passHash = users.First().PasswordHash;
            var result = hasher.VerifyHashedPassword(null!, passHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return new LoginResult(LoginResultType.WrongPassword);
            }

            var jwtSettings = config.GetSection("Jwt");
            byte[] key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
            var claims = Array.Empty<Claim>();

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"]!)),
                signingCredentials: new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
                );


            return new LoginResult(LoginResultType.Success, token, token.ValidTo);
        }

        public async Task<bool> SigninAsync(string username, string email, string password, CancellationToken ct)
        {
            var emailUsers = await userService.GetByEmailAsync(email, ct);
            if (emailUsers.Count != 0)
            {
                return false;
            }

            await userService.CreateAsync(new UserEntity()
            {
                Name = username,
                Email = email,
                PasswordHash = hasher.HashPassword(null!, password),
                IsEmailVerified = false
            }, ct);

            return true;
        }

    }
}
