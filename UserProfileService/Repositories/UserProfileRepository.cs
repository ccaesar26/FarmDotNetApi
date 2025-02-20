using Microsoft.EntityFrameworkCore;
using UserProfileService.Data;
using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories;

public class UserProfileRepository(UserProfileDbContext context) : IUserProfileRepository
{
    public async ValueTask<UserProfile?> GetByIdAsync(Guid id)
    {
        return await context.UserProfiles.FindAsync(id);
    }

    public async ValueTask<UserProfile> AddAsync(UserProfile userProfile)
    {
        var ret = await context.UserProfiles.AddAsync(userProfile);
        await context.SaveChangesAsync();
        return ret.Entity;
    }

    public async ValueTask UpdateAsync(UserProfile userProfile)
    {
        context.UserProfiles.Update(userProfile);
        await context.SaveChangesAsync();
    }

    public async ValueTask DeleteAsync(Guid id)
    {
        var userProfile = await context.UserProfiles.FindAsync(id);
        if (userProfile is not null)
        {
            context.UserProfiles.Remove(userProfile);
            await context.SaveChangesAsync();
        }
    }

    public async ValueTask<UserProfile?> GetByUserIdAsync(Guid userId)
    {
        return await context.UserProfiles.SingleOrDefaultAsync(x => x.UserId == userId);
    }
}