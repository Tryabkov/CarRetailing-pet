using Core.Interfaces;

namespace Core.Entities
{
    public class CarEntity : IDbEntity
    {
        public uint Id { get; set; }
        public string Mark { get; set; } = "";
        public string Model { get; set; } = "";
        public string Description { get; set; } = "";
        public int Mileage { get; set; }
        public short Year { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";

        public uint UserId { get; set; }
        public UserEntity User { get; set; }

        protected CarEntity() { }
        public CarEntity(uint userId, CreateCarDto car)
        {
            UserId = userId;
            Mark = car.Mark;
            Model = car.Model;
            Mileage = car.Mileage;
            Year = car.Year;
            Description = car.Description;
            Price = car.Price;
        }
    }
    
    public record CreateCarDto(string Mark, string Model, decimal Price, short Year, int Mileage, string Description = null!);
    public record UpdateCarDto(string? Mark = null, string? Model = null, decimal? Price = null, int? Year = null, int? Mileage = null, string? Description = null);
    public record ReturnCarDto(uint Id, string Mark, string Model, decimal Price, short Year, int Mileage, string Description = null!, ReturnUserDto User = null!);
}