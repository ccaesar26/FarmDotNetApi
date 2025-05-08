using System.Security.Cryptography;
using IdentityService.Models.Entities;
using IdentityService.Repositories.RefreshTokenRepository;
using IdentityService.Repositories.UserRepository;

namespace IdentityService.Services.RefreshTokenService;

public class RefreshTokenService(
    IRefreshTokenRepository repository,
    IUserRepository userRepository
) : IRefreshTokenService
{
    public async ValueTask<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
    {
        var randomNumberGenerator = RandomNumberGenerator.Create();
        var randomNumber = new byte[32];
        randomNumberGenerator.GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);

        var token = new RefreshToken
        {
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddMonths(6),
            UserId = userId,
            IsRevoked = false
        };

        await repository.CreateAsync(token);

        return token;
    }

    public async ValueTask<RefreshToken?> GetRefreshTokenAsync(string token) => await repository.GetByTokenAsync(token);

    public async ValueTask RevokeRefreshTokenAsync(RefreshToken refreshToken)
    {
        refreshToken.IsRevoked = true;
        await repository.UpdateAsync(refreshToken);
    }
}