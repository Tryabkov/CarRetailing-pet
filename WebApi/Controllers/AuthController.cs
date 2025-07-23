using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IConfiguration config, IAuthService authService, IUserService userService) : ControllerBase
    {
        private readonly PasswordHasher<UserEntity> hasher = new();

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken ct)
        {
            var users = await userService.GetByEmailAsync(loginDto.email, ct);
            if (users.Count == 0) { return BadRequest("Email not found."); }

            string passHash = users.First().PasswordHash;
            var result = hasher.VerifyHashedPassword(null!, passHash, loginDto.pass);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Wrong password.");
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


            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo});
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninDto signinDto, CancellationToken ct)
        {
            var emailUsers = await userService.GetByEmailAsync(signinDto.email, ct);     
            if (emailUsers.Count != 0)
            {
                return BadRequest("Email already registered.");
            }

            await userService.CreateAsync(new UserEntity()
            {
                Name = signinDto.username,
                Email = signinDto.email,
                PasswordHash = hasher.HashPassword(null!, signinDto.pass)
            }, ct);

            return Ok(signinDto.username);
        }
    }
}
