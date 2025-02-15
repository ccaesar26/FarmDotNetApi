using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories;

public interface IUserProfileRepository
{
    ValueTask<UserProfile?> GetByIdAsync(Guid id);
    
    ValueTask<UserProfile> AddAsync(UserProfile userProfile);
    
    ValueTask UpdateAsync(UserProfile userProfile);
    
    ValueTask DeleteAsync(Guid id);
    ValueTask<UserProfile?> GetByUserIdAsync(Guid userId);
}