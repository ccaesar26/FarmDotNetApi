namespace IdentityService.Models.Dtos;

public record UpdateUserResponse(
    string Id,
    string Username,
    string Email,
    string Role,
    string UserProfileId
);