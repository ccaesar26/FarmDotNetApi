using IdentityService.Models;
using Shared.FarmClaimTypes;

namespace IdentityService.Services.TokenService;

public interface ITokenService
{
    string GenerateJwtToken(User user);
    FarmClaimTypes.FarmClaimDto? ValidateJwt(string jwt);
    void SetAuthTokenInCookie(string token, HttpContext context);
    void SetRefreshTokenInCookie(string token, HttpContext context);
}
