using IdentityService.Models;
using IdentityService.Models.Dtos;

namespace IdentityService.Services.UserService;

public interface IUserService
{
    ValueTask<User?> GetUserAsync(Guid userId);
    ValueTask<User> UpdateUserAsync(UpdateUserRequest user);
    ValueTask<string> GetRoleAsync(string email);
    ValueTask UpdateFarmIdAsync(string userId, string farmId);
    ValueTask UpdateUserProfileAsync(string userId, string userProfileId);

    ValueTask<Guid> CreateUserAsync(string requestUsername, string requestEmail, string requestPassword,
        string requestRole, string? requestFarmId);

    ValueTask<IEnumerable<User>> GetWorkersAsync(Guid? farmId);
    ValueTask DeleteUserAsync(Guid userId);
    ValueTask<User?> GetWorkerAsync(Guid userId, Guid farmId);
}