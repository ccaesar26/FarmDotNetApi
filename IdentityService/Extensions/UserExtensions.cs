using IdentityService.Models;
using IdentityService.Models.Dtos;

namespace IdentityService.Extensions;

public static class UserExtensions
{
    public static UpdateUserRequest ToUpdateUserRequest(this User user)
        => new (user.Id.ToString(), user.Username, user.Email, user.Role.Name);

    public static UpdateUserResponse ToUpdateUserResponse(this User user)
        => new (user.Id.ToString(), user.Username, user.Email, user.Role.Name,
            user.UserProfileId.ToString() ?? "");
    
    public static UserDto ToDto(this User user)
        => new (user.Id.ToString(), user.Username, user.Email, user.Role.Name);
}