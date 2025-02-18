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

    public async ValueTask<string> GetRoleAsync(string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        return user?.Role.Name ?? string.Empty;
    }

    public async ValueTask UpdateFarmIdAsync(string userId, string farmId)
    {
        var user = await userRepository.GetUserByIdAsync(Guid.Parse(userId));
        if (user != null)
        {
            user.FarmId = Guid.Parse(farmId);
            await userRepository.UpdateUserAsync(user);
        }
    }

    public async ValueTask UpdateUserProfileAsync(string userId, string userProfileId)
    {
        var user = await userRepository.GetUserByIdAsync(Guid.Parse(userId));
        if (user != null)
        {
            user.UserProfileId = Guid.Parse(userProfileId);
            await userRepository.UpdateUserAsync(user);
        }
    }
}