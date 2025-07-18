﻿namespace PlantedCropsService.Models.Dtos;

public record GrowthStageEventUpdateDto(
    Guid Id,
    Guid CropId,
    string Stage,
    DateTime Timestamp,
    Guid RecordedByUserId,
    string RecordedByUserName
);