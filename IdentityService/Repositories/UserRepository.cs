using IdentityService.Data;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Repositories;

public class UserRepository(IdentityDbContext context) : IUserRepository
{
    public async ValueTask<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users
            .Include(u => u.Roles)
            .SingleOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}