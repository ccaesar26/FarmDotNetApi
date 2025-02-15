using UserProfileService.Models.Entities;
using UserProfileService.Repositories;

namespace UserProfileService.Services;

public class UserProfileService(IUserProfileRepository repository) : IUserProfileService
{
    public async ValueTask<UserProfile?> GetUserProfileAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async ValueTask<Guid> AddUserProfileAsync(string name, DateOnly dateOfBirth, string gender, Guid userId)
    {
        var userProfile = new UserProfile
        {
            Name = name,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            UserId = userId
        };
        
        userProfile = await repository.AddAsync(userProfile);
        
        return userProfile.Id;
    }

    public async ValueTask UpdateUserProfileAsync(Guid id, string name, DateOnly dateOfBirth, string gender)
    {
        var userProfile = new UserProfile
        {
            Id = id,
            Name = name,
            DateOfBirth = dateOfBirth,
            Gender = gender
        };
        
        await repository.UpdateAsync(userProfile);
    }

    public async ValueTask DeleteUserProfileAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }

    public async ValueTask<UserProfile?> GetUserProfileByUserIdAsync(Guid userId)
    {
        return await repository.GetByUserIdAsync(userId);
    }
}