using PlantedCropsService.Models.Dtos;
using PlantedCropsService.Models.Entities;

namespace PlantedCropsService.Mapping.Extensions;

public static class GrowthStageEventMappingExtensions
{
    public static GrowthStageEventDto ToDto(this GrowthStageEvent mapping) => new GrowthStageEventDto(
        mapping.Id,
        mapping.CropId,
        mapping.Stage,
        mapping.Timestamp,
        mapping.RecordedByUserId,
        mapping.RecordedByUserName
    );
    
    public static GrowthStageEvent ToEntity(this GrowthStageEventCreateDto mapping) => new GrowthStageEvent
    {
        CropId = mapping.CropId,
        Stage = mapping.Stage,
        Timestamp = mapping.Timestamp,
        RecordedByUserId = mapping.RecordedByUserId,
        RecordedByUserName = mapping.RecordedByUserName
    };
    
    public static GrowthStageEvent ToEntity(this GrowthStageEventUpdateDto mapping) => new GrowthStageEvent
    {
        Id = mapping.Id,
        CropId = mapping.CropId,
        Stage = mapping.Stage,
        Timestamp = mapping.Timestamp,
        RecordedByUserId = mapping.RecordedByUserId,
        RecordedByUserName = mapping.RecordedByUserName
    };
}