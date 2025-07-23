using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Entities
{
    public class UserEntity: IDbEntity
    {
        public uint Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsEmailVerified { get; set; } = false;
        public string PasswordHash { get; set; } = "";
        public string AvatarUrl { get; set; } = "";
        public string Bio { get; set; } = "";

        public decimal Balance { get; set; }
        public List<CarEntity> Cars { get; set; } = new();
    }
}
