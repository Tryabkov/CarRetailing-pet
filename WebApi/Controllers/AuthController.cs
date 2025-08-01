using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IAuthService authService, IConfiguration config) : ControllerBase
    {
        [Authorize]
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus() =>
        Ok(new
        {
            isLogged = true,
            username = User.Identity?.Name
        });
            

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken ct)
        {
            var loginResult = await authService.LoginAsync(loginDto.email, loginDto.password, ct);

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

                case LoginResultType.UserNotFound: return BadRequest();
                case LoginResultType.WrongPassword: return BadRequest();
                default: return NotFound();
            }

        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupDto SignupDto, CancellationToken ct)
        {
            bool isSuccess = await authService.SignupAsync(SignupDto.password, SignupDto.email, SignupDto.password, ct);
            if (!isSuccess)
            {
                return BadRequest("User already exists");
            }

            return Ok(SignupDto.username);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return Ok("Logged out");
        }
    }
}
