namespace FarmProfileService.Models.Dtos;

public record CreateFarmProfileRequest(
    string Name,
    string Country
);