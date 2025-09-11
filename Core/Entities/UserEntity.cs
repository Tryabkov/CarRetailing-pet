using Core.Interfaces;

namespace Core.Entities;

public class UserEntity : IDbEntity
{
    protected UserEntity()
    {
    }

    public UserEntity(CreateUserDto userDto)
    {
        Name = userDto.Name;
        Email = userDto.Email;
        IsEmailVerified = false;
        PhoneNumber = userDto.PhoneNumber;
        PasswordHash = userDto.PasswordHash;
        Bio = userDto.Bio;
    }

    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public bool IsEmailVerified { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string AvatarUrl { get; set; } = "";
    public string Bio { get; } = "";

    public decimal Balance { get; set; } = 0;
    public List<CarEntity> Cars { get; set; } = new();
    public uint Id { get; set; }
}

public record CreateUserDto(string Name, string Email, string PhoneNumber, string PasswordHash, string Bio = null!);

public record ReturnUserDto(string Name, string PhoneNumber, string Bio = null!);