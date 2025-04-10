namespace PlantedCropsService.Models.Dtos;

public record CropCreateDto(
    string Name,
    string BinomialName,
    string? CultivatedVariety,
    string? ImageLink,
    bool Perennial,
    DateOnly PlantingDate,
    DateOnly? ExpectedFirstHarvestDate,
    DateOnly? ExpectedLastHarvestDate,
    DateOnly? ExpectedFirstHarvestStartDate,
    DateOnly? ExpectedFirstHarvestEndDate,
    DateOnly? ExpectedLastHarvestStartDate,
    DateOnly? ExpectedLastHarvestEndDate,
    Guid FieldId,
    string? Surface,
    double Area,
    Guid CropCatalogId
);