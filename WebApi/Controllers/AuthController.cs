using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IAuthService authService, IConfiguration config) : ControllerBase
    {
        [Authorize]
        [HttpGet("status")]
        public Task<IActionResult> GetStatus() =>
            Task.FromResult<IActionResult>(Ok(new
            {
                isLogged = true,
                username = User.Identity?.Name
            }));
            

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken ct)
        {
            var loginResult = await authService.LoginAsync(loginDto.Email, loginDto.Password, ct);

            switch (loginResult.Type)
            {
                case LoginResultType.Success:
                {
                    Response.Cookies.Append("token", loginResult.Token!, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Path = "/",
                        SameSite = SameSiteMode.None,
                        //Domain = "192.168.0.105:5001",
                        Expires = DateTime.UtcNow.AddMinutes(int.Parse(config.GetSection("Jwt")["ExpiresInMinutes"]!))
                    });
                    return Ok();
                }

                case LoginResultType.UserNotFound:
                case LoginResultType.WrongPassword: return BadRequest();
                default: return NotFound();
            }

        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupDto signupDto, CancellationToken ct)
        {
            bool isSuccess = await authService.SignupAsync(signupDto.Password, signupDto.Email, signupDto.Password, ct);
            if (!isSuccess)
            {
                return BadRequest("User already exists");
            }

            return Ok(signupDto.Username);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return Ok("Logged out");
        }
    }
}
