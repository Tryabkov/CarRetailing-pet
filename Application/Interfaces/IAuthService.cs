using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResult> LoginAsync(string email, string password, CancellationToken ct);
        public Task<bool> SignupAsync(string username, string email, string password, CancellationToken ct);
    }
}
