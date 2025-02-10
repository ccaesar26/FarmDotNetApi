using UserProfileService.Models.Entities;
using UserProfileService.Repositories;

namespace UserProfileService.Services;

public class UserProfileService(IUserProfileRepository repository) : IUserProfileService
{
    public async ValueTask<UserProfile?> GetUserProfileAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async ValueTask AddUserProfileAsync(string name, DateOnly dateOfBirth, string gender, Guid userId)
    {
        var userProfile = new UserProfile
        {
            Name = name,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            UserId = userId
        };
        
        await repository.AddAsync(userProfile);
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
}