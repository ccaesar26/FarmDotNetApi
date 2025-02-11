using IdentityService.Models;

namespace IdentityService.Services;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(string email, string password);
    Task RegisterAsync(string username, string email, string password, string role, string? farmId);
}