using IdentityService.Models;

namespace IdentityService.Services.UserService;

public interface IUserService
{
    ValueTask<User?> GetUserAsync(Guid userId);
    ValueTask UpdateUserAsync(User user);
    ValueTask<string> GetRoleAsync(string email);
    ValueTask UpdateFarmIdAsync(string userId, string farmId);
    ValueTask UpdateUserProfileAsync(string userId, string userProfileId);

    ValueTask<Guid> CreateUserAsync(string requestUsername, string requestEmail, string requestPassword,
        string requestRole, string? requestFarmId);
}