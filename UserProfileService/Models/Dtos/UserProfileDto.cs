namespace UserProfileService.Models.Dtos;

public record UserProfileDto(
    Guid Id,
    string Name,
    DateOnly DateOfBirth,
    string Gender,
    string[] AttributeNames
);