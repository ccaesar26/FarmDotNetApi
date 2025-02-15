using IdentityService.Models;

namespace IdentityService.Services;

public interface IUserService
{
    ValueTask<User?> GetUserAsync(Guid userId);
}