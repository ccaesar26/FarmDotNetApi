using IdentityService.Models;

namespace IdentityService.Services.UserService;

public interface IUserService
{
    ValueTask<User?> GetUserAsync(Guid userId);
    ValueTask UpdateUserAsync(User user);
}