using IdentityService.Models;

namespace IdentityService.Services;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(string email, string password);
    Task RegisterManagerAsync(string username, string email, string password);
}