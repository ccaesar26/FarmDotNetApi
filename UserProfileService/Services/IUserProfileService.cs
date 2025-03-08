using UserProfileService.Models.Entities;

namespace UserProfileService.Services;

public interface IUserProfileService
{
    public ValueTask<UserProfile?> GetUserProfileAsync(Guid id);
    
    public ValueTask<Guid> AddUserProfileAsync(string name, DateOnly dateOfBirth, string gender, Guid userId,
        string role);
    
    public ValueTask UpdateUserProfileAsync(Guid id, string name, DateOnly dateOfBirth, string gender);
    
    public ValueTask DeleteUserProfileAsync(Guid id);
    ValueTask<UserProfile?> GetUserProfileByUserIdAsync(Guid userId);
    
    ValueTask AssignAttributesAsync(Guid userProfileId, List<string> attributeNames);
    ValueTask RemoveAttributesAsync(Guid userId, List<Guid> attributeIds);
    ValueTask<IEnumerable<ProfileAttribute>> GetAttributesAsync();
}