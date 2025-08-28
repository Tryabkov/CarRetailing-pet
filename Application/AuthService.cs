using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application
{
    public class AuthService(IConfiguration config, IUserService userService) : IAuthService
    {
        private readonly PasswordHasher<UserEntity> _hasher = new();

        public async Task<LoginResult> LoginAsync(string email, string password, CancellationToken ct)
        {
            var users = await userService.GetByEmailAsync(email, ct);
            if (users.Count == 0) 
            {
                return new LoginResult(LoginResultType.UserNotFound); 
            }
            var user = users.First();

            string passHash = user.PasswordHash;
            var result = _hasher.VerifyHashedPassword(null!, passHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return new LoginResult(LoginResultType.WrongPassword);
            }

            var claims = new[]
            {
                new Claim (type: ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (type: ClaimTypes.Name, user.Name),
                // new Claim (type: ClaimTypes.Role, "Authorized")
            };

            var jwtSettings = config.GetSection("Jwt");
            byte[] key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"]!)),
                signingCredentials: new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                ));

            var handler = new JwtSecurityTokenHandler();

            return new LoginResult(LoginResultType.Success, handler.WriteToken(token), token.ValidTo);
        }

        public async Task<bool> SignupAsync(string username, string email, string password, CancellationToken ct)
        {
            var emailUsers = await userService.GetByEmailAsync(email, ct);
            if (emailUsers.Count != 0)
            {
                return false;
            }
            
            await userService.CreateAsync(new UserEntity(
                new CreateUserDto
                (
                    Name: username,
                    Email: email,
                    PhoneNumber: email,
                    PasswordHash: _hasher.HashPassword(null, password)
                )), ct);
            return true;
        }

    }
}
