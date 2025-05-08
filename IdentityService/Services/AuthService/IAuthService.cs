using IdentityService.Models;

namespace IdentityService.Services.AuthService;

public interface IAuthService
{
    ValueTask<string?> AuthenticateAsync(string email, string password);
    ValueTask RegisterAsync(string username, string email, string password, string role, string? farmId);

    ValueTask<(bool Success, string ErrorMessage, string AccessToken, string RefreshToken)> LoginAsync(
        string email,
        string password
    );

    ValueTask<(bool Success, string ErrorMessage, string AccessToken, string RefreshToken)> RefreshAccessTokenAsync(
        string refreshToken
    );
}