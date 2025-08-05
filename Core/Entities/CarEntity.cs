using Core.Interfaces;

namespace Core.Entities
{
    public class CarEntity : IDbEntity
    {
        public uint Id { get; set; }
        public string Mark { get; set; } = "";
        public string Model { get; set; } = "";
        public string Description { get; set; } = "";
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
            Description = car.Description;
            Price = car.Price;
        }
    }
}