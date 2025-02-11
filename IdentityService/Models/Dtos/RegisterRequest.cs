namespace IdentityService.Models.Dtos;

public record RegisterRequest(
    string Username,
    string Email,
    string Password,
    string Role,
    string? FarmId
);