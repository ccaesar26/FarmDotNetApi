namespace UserProfileService.Models.Dtos;

public record GetUserProfileResponse(
    string Name,
    DateOnly DateOfBirth,
    string Gender
);