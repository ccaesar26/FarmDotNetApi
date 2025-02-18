using IdentityService.Models;
using IdentityService.Repositories;
using IdentityService.Services.TokenService;

namespace IdentityService.Services.AuthService;

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
    
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}