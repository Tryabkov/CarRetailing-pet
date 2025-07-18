using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CarEntity
    {
        public uint Id { get; set; }
        public string Mark { get; set; } = "";
        public string Model { get; set; } = "";
        public decimal Price { get; set; }

    }
}