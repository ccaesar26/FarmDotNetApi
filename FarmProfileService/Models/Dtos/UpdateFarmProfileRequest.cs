namespace FarmProfileService.Models.Dtos;

public record UpdateFarmProfileRequest(
    Guid Id,
    string Name,
    string Country
);