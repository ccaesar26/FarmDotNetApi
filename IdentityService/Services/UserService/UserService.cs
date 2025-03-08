using IdentityService.Models;
using IdentityService.Models.Entities;
using IdentityService.Repositories;
using IdentityService.Repositories.RoleRepository;

namespace IdentityService.Services.UserService;

public class UserService(IUserRepository userRepository, IRoleRepository roleRepository) : IUserService
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

    public async ValueTask<Guid> CreateUserAsync(string requestUsername, string requestEmail, string requestPassword,
        string requestRole,
        string? requestFarmId)
    {
        var role = await roleRepository.GetRoleByNameAsync(requestRole);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found!");
        }
        
        var user = new User
        {
            Username = requestUsername,
            Email = requestEmail,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestPassword),
            Role = await roleRepository.GetRoleByNameAsync(requestRole) ?? throw new InvalidOperationException(),
            FarmId = requestFarmId == null ? null : Guid.Parse(requestFarmId)
        };

        return await userRepository.CreateUserAsync(user);
    }
}