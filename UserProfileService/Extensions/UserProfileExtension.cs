using UserProfileService.Models.Dtos;
using UserProfileService.Models.Entities;

namespace UserProfileService.Extensions;

public static class UserProfileExtension
{
    public static GetUserProfileResponse ToDto(this UserProfile userProfile)
    {
        return new GetUserProfileResponse(
            userProfile.Name,
            userProfile.DateOfBirth,
            userProfile.Gender
        );
    }
}