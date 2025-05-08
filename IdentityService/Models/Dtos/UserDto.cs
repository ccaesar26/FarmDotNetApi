namespace IdentityService.Models.Dtos;

public record UserDto(
    string Id,
    string Username,
    string Email,
    string Role
);