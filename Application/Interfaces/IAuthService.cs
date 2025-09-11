using Core.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<LoginResult> LoginAsync(string email, string password, CancellationToken ct);
    Task<bool> SignupAsync(string username, string email, string password, CancellationToken ct);
}