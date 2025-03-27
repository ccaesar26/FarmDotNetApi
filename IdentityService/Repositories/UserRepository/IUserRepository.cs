using IdentityService.Models;

namespace IdentityService.Repositories.UserRepository;

public interface IUserRepository
{
    public ValueTask<User?> GetUserByEmailAsync(string email);
    
    public ValueTask<Guid> AddUserAsync(User user);
    
    public ValueTask<User> UpdateUserAsync(User user);
    Task<User?> GetUserByIdAsync(Guid id);
    ValueTask<Guid> CreateUserAsync(User user);
    ValueTask<IEnumerable<User>> GetUsersByFarmIdAsync(Guid? farmId);
    ValueTask DeleteUserAsync(Guid userId);
}