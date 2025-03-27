using UserProfileService.Models.Entities;
using UserProfileService.Repositories.AttributeCategoryRepository;
using UserProfileService.Repositories.ProfileAttributeRepository;
using UserProfileService.Repositories.UserProfileRepository;

namespace UserProfileService.Services.UserProfileService;

public class UserProfileService(
    IUserProfileRepository userProfileRepository,
    IProfileAttributeRepository profileAttributeRepository,
    IAttributeCategoryRepository attributeCategoryRepository,
    ILogger<UserProfileService> logger
) : IUserProfileService
{
    public async ValueTask<UserProfile?> GetUserProfileAsync(Guid id)
    {
        return await userProfileRepository.GetByIdAsync(id);
    }

    public async ValueTask<Guid> AddUserProfileAsync(string name, DateOnly dateOfBirth, string gender, Guid userId,
        string role)
    {
        var userProfile = new UserProfile
        {
            Name = name,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            UserId = userId
        };

        // Add "Manager" attribute if necessary
        if (role == "Manager") // Use the provided role!
        {
            var managerAttribute =
                await profileAttributeRepository.GetByNameAndCategoryNameAsync("Manager", "Administrative");

            if (managerAttribute != null)
            {
                userProfile.ProfileAttributes.Add(managerAttribute);
            }
            else
            {
                throw new InvalidOperationException("Manager attribute not found!");
            }
        }

        userProfile = await userProfileRepository.AddAsync(userProfile);

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

        await userProfileRepository.UpdateAsync(userProfile);
    }

    public async ValueTask DeleteUserProfileAsync(Guid id)
    {
        await userProfileRepository.DeleteAsync(id);
    }

    public async ValueTask<UserProfile?> GetUserProfileByUserIdAsync(Guid userId)
    {
        return await userProfileRepository.GetByUserIdAsync(userId);
    }

    public async ValueTask AssignAttributesAsync(Guid userProfileId, List<string> attributeNames)
    {
        var userProfile = await userProfileRepository.GetByUserProfileIdAsync(userProfileId);

        if (userProfile == null)
        {
            throw new ArgumentException($"User profile for user ID {userProfileId} not found.");
        }

        foreach (var attributeName in attributeNames)
        {
            var attribute = await profileAttributeRepository.GetByNameAsync(attributeName);

            if (attribute != null)
            {
                await userProfileRepository.AddAttribute(userProfile, attribute);
            }
            else
            {
                throw new ArgumentException($"Attribute {attributeName} not found.");
            }
        }

        logger.LogInformation("Attributes assigned to UserProfile: {UserProfileId}", userProfile.Id);
    }

    public async ValueTask RemoveAttributesAsync(Guid userId, List<Guid> attributeIds)
    {
        var userProfile = await userProfileRepository.GetByUserProfileIdAsync(userId);

        if (userProfile == null)
        {
            throw new ArgumentException($"User profile for user ID {userId} not found.");
        }

        if (attributeIds.Count != 0)
        {
            // Find the attributes to remove (IMPORTANT: use .ToList() to avoid modifying the collection while iterating)
            var attributesToRemove = userProfile.ProfileAttributes.Where(pa => attributeIds.Contains(pa.Id)).ToList();

            foreach (var attribute in attributesToRemove)
            {
                userProfile.ProfileAttributes.Remove(attribute);
            }
        }

        logger.LogInformation("Attributes removed from UserProfile: {UserProfileId}", userProfile.Id);
    }

    public async ValueTask<IEnumerable<ProfileAttribute>> GetAttributesAsync()
    {
        return await profileAttributeRepository.GetAllAsync();
    }

    public async ValueTask UpdateAttributesAsync(Guid userProfileId, List<string> attributeNames)
    {
        var userProfile = await userProfileRepository.GetByUserProfileIdAsync(userProfileId);

        if (userProfile == null)
        {
            throw new ArgumentException($"User profile for user ID {userProfileId} not found.");
        }

        // Remove all existing attributes
        userProfile.ProfileAttributes.Clear();

        // Assign new attributes
        await AssignAttributesAsync(userProfileId, attributeNames);
    }
}