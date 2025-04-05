namespace PlantedCropsService.Models.Dtos;

public record GrowthStageEventCreateDto(
    Guid CropId,
    string Stage,
    DateTime Timestamp,
    Guid RecordedByUserId,
    string RecordedByUserName
);