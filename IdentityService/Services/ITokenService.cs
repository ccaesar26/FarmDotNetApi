using IdentityService.Models;

namespace IdentityService.Services;

public interface ITokenService
{
    string GenerateJwtToken(User user);
    // string GenerateRefreshToken();
}
