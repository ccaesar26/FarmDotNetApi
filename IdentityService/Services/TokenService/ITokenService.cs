using IdentityService.Models;

namespace IdentityService.Services.TokenService;

public interface ITokenService
{
    string GenerateJwtToken(User user);
    // string GenerateRefreshToken();
}
