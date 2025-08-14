using Core.Interfaces;

namespace Core.Entities
{
    public class UserEntity: IDbEntity
    {
        public uint Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsEmailVerified { get; set; } = false;
        public string PhoneNumber { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string AvatarUrl { get; set; } = "";
        public string Bio { get; } = "";

        public decimal Balance { get; set; } = 0;
        public List<CarEntity> Cars { get; set; } = new();
        
        protected UserEntity() { }

        public UserEntity(CreateUserDto userDto)
        {
            Name = userDto.Name;
            Email = userDto.Email;
            IsEmailVerified = false;
            PhoneNumber = userDto.PhoneNumber;
            PasswordHash = userDto.PasswordHash;
            Bio = userDto.Bio;
        }
    }
    
    public record CreateUserDto(string Name, string Email, string PhoneNumber, string PasswordHash, string Bio = null!);
    public record ReturnUserDto(string Name, string PhoneNumber, string Bio = null!);
}
