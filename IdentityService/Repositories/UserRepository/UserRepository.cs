using IdentityService.Data;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Repositories;

public class UserRepository(IdentityDbContext context) : IUserRepository
{
    public async ValueTask<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task AddUserAsync(User? user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User? user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Id == id);
    }

    public async ValueTask<Guid> CreateUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user.Id;
    }

    public async ValueTask<IEnumerable<User>> GetUsersByFarmIdAsync(Guid? farmId)
    {
        return await context.Users
            .Include(u => u.Role)
            .Where(u => u.FarmId == farmId)
            .ToListAsync();
    }
}