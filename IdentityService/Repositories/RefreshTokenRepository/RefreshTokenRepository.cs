using IdentityService.Data;
using IdentityService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Repositories.RefreshTokenRepository;

public class RefreshTokenRepository(IdentityDbContext context) : IRefreshTokenRepository
{
    public async ValueTask<RefreshToken?> GetByTokenAsync(string token) => await context.RefreshTokens
        .Include(rt => rt.User)
        .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked && rt.ExpiryDate > DateTime.UtcNow);

    public async ValueTask<RefreshToken?> GetByUserIdAsync(Guid userId)
    {
        var refreshToken = await context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.UserId == userId);

        return refreshToken;
    }

    public async ValueTask<RefreshToken> CreateAsync(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();

        return refreshToken;
    }

    public async ValueTask UpdateAsync(RefreshToken refreshToken)
    {
        context.RefreshTokens.Update(refreshToken);
        await context.SaveChangesAsync();
    }
}