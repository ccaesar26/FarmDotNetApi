namespace UserProfileService.Models.Dtos;

public record UpdateUserProfileRequest(
    Guid Id,
    string Name,
    DateOnly DateOfBirth,
    string Gender
);