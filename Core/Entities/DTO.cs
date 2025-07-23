using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public record LoginDto(string email, string pass);
    public record SigninDto(string username, string email, string pass);
}
