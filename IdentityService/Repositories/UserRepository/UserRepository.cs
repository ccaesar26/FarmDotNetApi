using IdentityService.Data;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Repositories.UserRepository;

public class UserRepository(IdentityDbContext context) : IUserRepository
{
    public async ValueTask<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Email == email);
    }
    
    public async ValueTask<Guid> AddUserAsync(User user)
    {
        var addedUser = (await context.Users.AddAsync(user)).Entity;
        await context.SaveChangesAsync();
        return addedUser.Id;
    }

    public async ValueTask<User> UpdateUserAsync(User user)
    {
        var returnedUser = await context.Users.FindAsync(user.Id);
        if (returnedUser == null)
        {
            throw new InvalidOperationException("User not found!");
        }
        
        returnedUser.Username = user.Username;
        returnedUser.Email = user.Email;
        returnedUser.Role = user.Role;
        
        await context.SaveChangesAsync();
        
        await context.Entry(returnedUser).ReloadAsync();
        
        return returnedUser;
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

    public async ValueTask DeleteUserAsync(Guid userId)
    {
        var user = await context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found!");
        }
        
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}