using IdentityService.Models.Entities;

namespace IdentityService.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository
{
    ValueTask<RefreshToken?> GetByTokenAsync(string token);
    
    ValueTask<RefreshToken?> GetByUserIdAsync(Guid userId);
    
    ValueTask<RefreshToken> CreateAsync(RefreshToken refreshToken);
    
    ValueTask UpdateAsync(RefreshToken refreshToken);
}