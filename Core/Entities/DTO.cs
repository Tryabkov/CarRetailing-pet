using System.IdentityModel.Tokens.Jwt;

namespace Core.Entities
{
    public record LoginDto(string email, string password);
    public record SignupDto(string username, string email, string password);
    public record CreateCarDto(string mark, decimal price, string model = null!, string description = null!);
    public record ReturnCarDto(uint id, string mark, decimal price, string model = null!, string description = null!, ReturnUserDto user = null!);
    public record CreateUserDto(string name, string email, string bio = null!);
    public record ReturnUserDto(string name, string bio = null!);
    public record LoginResult(LoginResultType Type, string? Token = null, DateTime? Expires = null); 
}
