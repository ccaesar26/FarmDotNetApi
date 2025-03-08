using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories.UserProfileRepository;

public interface IUserProfileRepository
{
    ValueTask<UserProfile?> GetByIdAsync(Guid id);
    
    ValueTask<UserProfile> AddAsync(UserProfile userProfile);

    ValueTask AddAttribute(UserProfile userProfile, ProfileAttribute profileAttribute);
    
    ValueTask UpdateAsync(UserProfile userProfile);
    
    ValueTask DeleteAsync(Guid id);
    
    ValueTask<UserProfile?> GetByUserProfileIdAsync(Guid userProfileId);
    
    Task<UserProfile?> GetByUserIdAsync(Guid userId);
}