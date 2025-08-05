namespace Core.Entities
{
    public record LoginDto(string Email, string Password);
    public record SignupDto(string Username, string Email, string Password);
    public record CreateCarDto(string Mark, decimal Price, string Model = null!, string Description = null!);
    public record ReturnCarDto(uint Id, string Mark, decimal Price, string Model = null!, string Description = null!, ReturnUserDto User = null!);
    public record CreateUserDto(string Name, string Email, string Bio = null!);
    public record ReturnUserDto(string Name, string Bio = null!);
    public record LoginResult(LoginResultType Type, string? Token = null, DateTime? Expires = null); 
}
