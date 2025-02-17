using IdentityService.Models;
using IdentityService.Repositories;

namespace IdentityService.Services.UserService;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async ValueTask<User?> GetUserAsync(Guid userId)
    {
        return await userRepository.GetUserByIdAsync(userId);
    }

    public async ValueTask UpdateUserAsync(User user)
    {
        await userRepository.UpdateUserAsync(user);
    }
}