using System.IdentityModel.Tokens.Jwt;

namespace Core.Entities
{
    public record LoginDto(string email, string password);
    public record SigninDto(string username, string email, string password);
    public record CreateCarDto(uint userId, string mark, decimal price, string model = null!, string description = null!);
    public record ReturnCarDto(string mark, decimal price, string model = null!, string description = null!, ReturnUserDto user = null!);
    public record CreateUserDto(string name, string email, string bio = null!);
    public record ReturnUserDto(string name, string bio = null!);
    public record LoginResult(LoginResultType Type, JwtSecurityToken? Token = null, DateTime? Expires = null); 
}
