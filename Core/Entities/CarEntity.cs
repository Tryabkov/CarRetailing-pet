using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public CarEntity(CreateCarDto car)
        {
            UserId = car.userId;
            Mark = car.mark;
            Model = car.model;
            Description = car.description;
            Price = car.price;
        }
    }
}