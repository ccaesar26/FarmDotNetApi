using IdentityService.Models;
using IdentityService.Models.Dtos;
using IdentityService.Models.Entities;
using IdentityService.Repositories;
using IdentityService.Repositories.RoleRepository;
using IdentityService.Repositories.UserRepository;

namespace IdentityService.Services.UserService;

public class UserService(IUserRepository userRepository, IRoleRepository roleRepository) : IUserService
{
    public async ValueTask<User?> GetUserAsync(Guid userId)
    {
        return await userRepository.GetUserByIdAsync(userId);
    }

    public async ValueTask<User> UpdateUserAsync(UpdateUserRequest user)
    {
        var role = await roleRepository.GetRoleByNameAsync(user.RoleName);
        var passwordHash = (await userRepository.GetUserByIdAsync(Guid.Parse(user.Id)))?.PasswordHash;
        var updatedUser = await userRepository.UpdateUserAsync(new User
        {
            Id = Guid.Parse(user.Id),
            Username = user.Username,
            Email = user.Email,
            PasswordHash = passwordHash ?? throw new InvalidOperationException("User not found!"),
            Role = role ?? throw new InvalidOperationException("Role not found!")
        });
        return updatedUser;
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

    public async ValueTask<IEnumerable<User>> GetWorkersAsync(Guid? farmId)
    {
        return (await userRepository.GetUsersByFarmIdAsync(farmId)).Where(u => u.Role.Name == "Worker");
    }

    public async ValueTask DeleteUserAsync(Guid userId)
    { 
        await userRepository.DeleteUserAsync(userId);
    }

    public async ValueTask<User?> GetWorkerAsync(Guid userId, Guid farmId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if (user == null || user.FarmId != farmId)
        {
            return null;
        }
        
        if (user.Role.Name != "Worker")
        {
            throw new InvalidOperationException("User is not a worker.");
        }

        return user;
    }
}