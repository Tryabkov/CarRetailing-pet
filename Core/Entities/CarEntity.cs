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
        public decimal Price { get; set; }

        public uint UserId { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}