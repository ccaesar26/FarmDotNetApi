using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Common;
using IdentityService.Data;
using IdentityService.Models;
using IdentityService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Services;

public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
{
    public async Task<string?> AuthenticateAsync(string email, string password)
    {
        var user = await userRepository.GetUserByEmailAsync(email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return GenerateJwtToken(user);
    }

    public async Task RegisterAsync(string username, string email, string password, string? farmId)
    {
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = farmId != null ? new Role { Name = RoleNameTypes.Worker } : new Role { Name = RoleNameTypes.Manager }
        };

        if (farmId != null)
        {
            user.FarmId = new Guid(farmId);
        }
        
        await userRepository.AddUserAsync(user);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException());

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(FarmClaimTypes.UserId, user.Id.ToString()),
                new Claim(FarmClaimTypes.Email, user.Email),
                new Claim(FarmClaimTypes.Role, user.Role.Name),
                new Claim(FarmClaimTypes.FarmId, user.FarmId.ToString() ?? string.Empty)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["AccessTokenExpiryMinutes"] ?? "60")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}