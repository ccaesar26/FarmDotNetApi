using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Common;
using IdentityService.Data;
using IdentityService.Models;
using IdentityService.Repositories;
using Microsoft.IdentityModel.Tokens;
using Shared.FarmClaimTypes;

namespace IdentityService.Services;

public class AuthService(IUserRepository userRepository, ITokenService tokenService) : IAuthService
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
            Role = new Role { Name = role }
        };
        
        if (farmId != null)
        {
            user.FarmId = Guid.Parse(farmId);
        }
        
        await userRepository.AddUserAsync(user);
    }

    public async ValueTask<string> GetRoleAsync(string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        return user?.Role.Name ?? string.Empty;
    }

    public async ValueTask UpdateFarmIdAsync(string userId, string farmId)
    {
        var user = await userRepository.GetUserByIdAsync(Guid.Parse(userId));
        if (user != null)
        {
            user.FarmId = Guid.Parse(farmId);
            await userRepository.UpdateUserAsync(user);
        }
    }

    public async ValueTask UpdateUserProfileAsync(string userId, string userProfileId)
    {
        var user = await userRepository.GetUserByIdAsync(Guid.Parse(userId));
        if (user != null)
        {
            user.UserProfileId = Guid.Parse(userProfileId);
            await userRepository.UpdateUserAsync(user);
        }
    }
    
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}