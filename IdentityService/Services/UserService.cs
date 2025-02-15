using IdentityService.Models;
using IdentityService.Repositories;

namespace IdentityService.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async ValueTask<User?> GetUserAsync(Guid userId)
    {
        return await userRepository.GetUserByIdAsync(userId);
    }
}