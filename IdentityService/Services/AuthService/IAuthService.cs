using IdentityService.Models;

namespace IdentityService.Services.AuthService;

public interface IAuthService
{
    ValueTask<string?> AuthenticateAsync(string email, string password);
    ValueTask RegisterAsync(string username, string email, string password, string role, string? farmId);
    ValueTask<string> GetRoleAsync(string email);
    ValueTask UpdateFarmIdAsync(string userId, string farmId);
    ValueTask UpdateUserProfileAsync(string userId, string userProfileId);
}