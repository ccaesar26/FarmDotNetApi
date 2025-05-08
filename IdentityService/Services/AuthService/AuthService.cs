using IdentityService.Models;
using IdentityService.Repositories;
using IdentityService.Repositories.RoleRepository;
using IdentityService.Repositories.UserRepository;
using IdentityService.Services.RefreshTokenService;
using IdentityService.Services.TokenService;
using Microsoft.VisualBasic;

namespace IdentityService.Services.AuthService;

public class AuthService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IRoleRepository roleRepository,
    IRefreshTokenService refreshTokenService
) : IAuthService
{
    public async ValueTask<string?> AuthenticateAsync(string email, string password)
    {
        var user = await userRepository.GetUserByEmailAsync(email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return tokenService.GenerateJwtToken(user);
    }

    public async ValueTask RegisterAsync(string username, string email, string password, string role, string? farmId)
    {
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = await roleRepository.GetRoleByNameAsync(role) ?? throw new InvalidOperationException()
        };

        if (farmId != null)
        {
            user.FarmId = Guid.Parse(farmId);
        }

        await userRepository.AddUserAsync(user);
    }

    public async ValueTask<(bool Success, string ErrorMessage, string AccessToken, string RefreshToken)> LoginAsync(string email, string password)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            return (false, "Invalid email or password", string.Empty, string.Empty);
        }
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return (false, "Invalid email or password", string.Empty, string.Empty);
        }
        
        var accessToken = tokenService.GenerateJwtToken(user);
        var refreshTokenEntity = await refreshTokenService.GenerateRefreshTokenAsync(user.Id);
        var refreshToken = refreshTokenEntity.Token;
        
        return (true, string.Empty, accessToken, refreshToken);
    }

    public async ValueTask<(bool Success, string ErrorMessage, string AccessToken, string RefreshToken)> RefreshAccessTokenAsync(string refreshToken)
    {
        var refreshTokenEntity = await refreshTokenService.GetRefreshTokenAsync(refreshToken);
        if (refreshTokenEntity is null)
        {
            return (false, "Invalid refresh token", string.Empty, string.Empty);
        }

        var user = await userRepository.GetUserByIdAsync(refreshTokenEntity.UserId);
        if (user == null)
        {
            return (false, "User not found", string.Empty, string.Empty);
        }
        
        await refreshTokenService.RevokeRefreshTokenAsync(refreshTokenEntity);

        var newAccessToken = tokenService.GenerateJwtToken(user);
        var newRefreshTokenEntity = await refreshTokenService.GenerateRefreshTokenAsync(user.Id);
        var newRefreshToken = newRefreshTokenEntity.Token;

        return (true, string.Empty, newAccessToken, newRefreshToken);
    }
}