namespace CropCatalogService.Model.Dtos;

public record CropCatalogDto(
    Guid Id,
    string Name,
    string BinomialName,
    bool IsPerennial,
    int? DaysToFirstHarvest,
    int? DaysToLastHarvest,
    int? MinMonthsToBearFruit,
    int? MaxMonthsToBearFruit,
    string? HarvestSeasonStart,
    string? HarvestSeasonEnd,
    string? Description,
    string? WikipediaLink,
    string? ImageLink,
    string? SunRequirements,
    string? SowingMethod
);