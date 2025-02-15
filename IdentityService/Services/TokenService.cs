using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Models;
using Microsoft.IdentityModel.Tokens;
using Shared.FarmClaimTypes;

namespace IdentityService.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateJwtToken(User user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException());

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(FarmClaimTypes.UserId, user.Id.ToString()),
                new Claim(FarmClaimTypes.Email, user.Email),
                new Claim(FarmClaimTypes.Role, user.Role.Name),
                new Claim(FarmClaimTypes.FarmId, user.FarmId?.ToString() ?? string.Empty),
                new Claim(FarmClaimTypes.UserProfileId, user.UserProfileId?.ToString() ?? string.Empty)
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
}