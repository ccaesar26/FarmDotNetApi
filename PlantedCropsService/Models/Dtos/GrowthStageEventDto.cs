namespace PlantedCropsService.Models.Dtos;

public record GrowthStageEventDto(
    Guid Id,
    Guid CropId,
    string Stage,
    DateTime Timestamp,
    Guid RecordedByUserId,
    string RecordedByUserName
);