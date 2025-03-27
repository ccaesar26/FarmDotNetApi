namespace IdentityService.Models.Dtos;

public record UpdateUserRequest(
    string Id,
    string Username,
    string Email,
    string RoleName
);