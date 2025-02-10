using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories;

public interface IUserProfileRepository
{
    ValueTask<UserProfile?> GetByIdAsync(Guid id);
    
    ValueTask AddAsync(UserProfile userProfile);
    
    ValueTask UpdateAsync(UserProfile userProfile);
    
    ValueTask DeleteAsync(Guid id);
}