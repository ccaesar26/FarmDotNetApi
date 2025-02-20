using IdentityService.Models;
using Shared.FarmClaimTypes;

namespace IdentityService.Services.TokenService;

public interface ITokenService
{
    string GenerateJwtToken(User user);
    string GenerateJwtToken(FarmClaimTypes.FarmClaimDto user);
    FarmClaimTypes.FarmClaimDto? ValidateJwt(string jwt);
    void SetTokenInCookie(string token, HttpContext context);
}
