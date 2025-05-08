using IdentityService.Models.Entities;

namespace IdentityService.Services.RefreshTokenService;

public interface IRefreshTokenService
{
    public ValueTask<RefreshToken> GenerateRefreshTokenAsync(Guid userId);
    public ValueTask<RefreshToken?> GetRefreshTokenAsync(string token);
    public ValueTask RevokeRefreshTokenAsync(RefreshToken refreshToken);
}