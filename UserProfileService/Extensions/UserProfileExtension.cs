using UserProfileService.Models.Dtos;
using UserProfileService.Models.Entities;

namespace UserProfileService.Extensions;

public static class UserProfileExtension
{
    public static UserProfileDto ToDto(this UserProfile userProfile)
    {
        return new UserProfileDto(
            userProfile.Id,
            userProfile.Name,
            userProfile.DateOfBirth,
            userProfile.Gender,
            userProfile.ProfileAttributes.Select(pa => pa.Name).ToArray()
        );
    }
}