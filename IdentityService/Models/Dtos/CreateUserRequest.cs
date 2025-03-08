namespace IdentityService.Models.Dtos;

public record CreateUserRequest(
    string Username,
    string Email,
    string Password,
    string Role,
    string? FarmId = null
);