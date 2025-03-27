namespace CropCatalogService.Model.Dtos;

public record UpdateCropCatalogDto(
    Guid Id,
    string Name,
    string BinomialName,
    bool IsPerennial,
    int? DaysToFirstHarvest,
    int? DaysToLastHarvest,
    DateOnly? HarvestSeasonStart,
    DateOnly? HarvestSeasonEnd,
    string? Description,
    string? WikipediaLink,
    string? ImageLink,
    string? SunRequirements,
    string? SowingMethod
);