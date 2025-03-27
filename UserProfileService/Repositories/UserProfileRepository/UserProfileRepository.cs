using Microsoft.EntityFrameworkCore;
using UserProfileService.Data;
using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories.UserProfileRepository;

public class UserProfileRepository(UserProfileDbContext context) : IUserProfileRepository
{
    public async ValueTask<UserProfile?> GetByIdAsync(Guid id)
    {
        return await context.UserProfiles
            .Include(up => up.ProfileAttributes)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async ValueTask<UserProfile> AddAsync(UserProfile userProfile)
    {
        var ret = await context.UserProfiles.AddAsync(userProfile);
        await context.SaveChangesAsync();
        return ret.Entity;
    }
    
    public async ValueTask AddAttribute(UserProfile userProfile, ProfileAttribute profileAttribute)
    {
        // Avoid duplicate attributes
        if (userProfile.ProfileAttributes.Any(pa => pa.Name == profileAttribute.Name))
        {
            return;
        }
        
        userProfile.ProfileAttributes.Add(profileAttribute);
        await context.SaveChangesAsync();
    }

    public async ValueTask UpdateAsync(UserProfile userProfile)
    {
        var existingUserProfile = await context.UserProfiles.FindAsync(userProfile.Id);
        if (existingUserProfile is null)
        {
            return;
        }
        
        existingUserProfile.Name = userProfile.Name;
        existingUserProfile.DateOfBirth = userProfile.DateOfBirth;
        existingUserProfile.Gender = userProfile.Gender;
        
        await context.SaveChangesAsync();

        await context.Entry(existingUserProfile).ReloadAsync();
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

    public async ValueTask<UserProfile?> GetByUserProfileIdAsync(Guid userProfileId)
    {
        return await context.UserProfiles
            .Include(up => up.ProfileAttributes)
            .SingleOrDefaultAsync(x => x.Id == userProfileId);
    }

    public async Task<UserProfile?> GetByUserIdAsync(Guid userId)
    {
        return await context.UserProfiles
            .Include(up => up.ProfileAttributes)
            .SingleOrDefaultAsync(x => x.UserId == userId);
    }
}