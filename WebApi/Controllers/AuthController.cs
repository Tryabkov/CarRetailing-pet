using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken ct)
        {
            var loginResult = await authService.LoginAsync(loginDto.email, loginDto.password, ct);

            switch (loginResult.Type)
            {
                case LoginResultType.Success: return Ok(loginResult.Token);
                case LoginResultType.UserNotFound: return Unauthorized(loginResult.Token);
                case LoginResultType.WrongPassword: return BadRequest(loginResult.Token);
                default: return NotFound();
            }

        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninDto signinDto, CancellationToken ct)
        {
            bool isSuccess = await authService.SigninAsync(signinDto.password, signinDto.email, signinDto.password, ct);
            if (!isSuccess)
            {
                return BadRequest("User already exists");
            }

            return Ok(signinDto.username);
        }
    }
}
