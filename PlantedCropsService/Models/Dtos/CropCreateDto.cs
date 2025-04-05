namespace PlantedCropsService.Models.Dtos;

public record CropCreateDto(
    string Name,
    string BinomialName,
    string? CultivatedVariety,
    string? ImageLink,
    bool Perrenial,
    DateOnly ExpectedFirstHarvestDate,
    DateOnly ExpectedLastHarvestDate,
    Guid FieldId,
    string? Surface,
    double Area,
    Guid CropCatalogId
);