using IdentityService.Models;

namespace IdentityService.Repositories;

public interface IUserRepository
{
    public ValueTask<User?> GetUserByEmailAsync(string email);
    
    public Task AddUserAsync(User? user);
    
    public Task UpdateUserAsync(User? user);
    Task<User?> GetUserByIdAsync(Guid id);
}