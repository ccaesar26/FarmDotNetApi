namespace CropCatalogService.Model.Dtos;

public record CreateCropCatalogDto(
    string Name,
    string BinomialName,
    bool IsPerennial,
    int? DaysToFirstHarvest,
    int? DaysToLastHarvest,
    int? MinMonthsToBearFruit,
    int? MaxMonthsToBearFruit,
    DateOnly? HarvestSeasonStart,
    DateOnly? HarvestSeasonEnd,
    string? Description,
    string? WikipediaLink,
    string? ImageLink,
    string? SunRequirements,
    string? SowingMethod
);