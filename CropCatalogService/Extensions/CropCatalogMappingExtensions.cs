using System.Runtime.InteropServices.JavaScript;
using CropCatalogService.Model.Dtos;
using CropCatalogService.Model.Entities;

namespace CropCatalogService.Extensions;

public static class CropCatalogMappingExtensions
{
    public static CropCatalogEntry ToEntity(this CropCatalogDto cropCatalogDto)
    {
        return new CropCatalogEntry
        {
            Id = cropCatalogDto.Id,
            Name = cropCatalogDto.Name,
            BinomialName = cropCatalogDto.BinomialName,
            IsPerennial = cropCatalogDto.IsPerennial,
            DaysToFirstHarvest = cropCatalogDto.DaysToFirstHarvest,
            DaysToLastHarvest = cropCatalogDto.DaysToLastHarvest,
            MinMonthsToBearFruit = cropCatalogDto.MinMonthsToBearFruit,
            MaxMonthsToBearFruit = cropCatalogDto.MaxMonthsToBearFruit,
            HarvestSeasonStart = DateOnly.TryParse(cropCatalogDto.HarvestSeasonStart, out var start) ? start : null,
            HarvestSeasonEnd = DateOnly.TryParse(cropCatalogDto.HarvestSeasonEnd, out var end) ? end : null,
            Description = cropCatalogDto.Description,
            WikipediaLink = cropCatalogDto.WikipediaLink,
            ImageLink = cropCatalogDto.ImageLink,
            SunRequirements = cropCatalogDto.SunRequirements,
            SowingMethod = cropCatalogDto.SowingMethod
        };
    }

    public static CropCatalogEntry ToEntity(this CreateCropCatalogDto dto)
    {
        return new CropCatalogEntry
        {
            Name = dto.Name,
            BinomialName = dto.BinomialName,
            IsPerennial = dto.IsPerennial,
            DaysToFirstHarvest = dto.DaysToFirstHarvest,
            DaysToLastHarvest = dto.DaysToLastHarvest,
            MinMonthsToBearFruit = dto.MinMonthsToBearFruit,
            MaxMonthsToBearFruit = dto.MaxMonthsToBearFruit,
            HarvestSeasonStart = dto.HarvestSeasonStart,
            HarvestSeasonEnd = dto.HarvestSeasonEnd,
            Description = dto.Description,
            WikipediaLink = dto.WikipediaLink,
            ImageLink = dto.ImageLink,
            SunRequirements = dto.SunRequirements,
            SowingMethod = dto.SowingMethod
        };
    }

    public static CropCatalogEntry ToEntity(this UpdateCropCatalogDto dto)
    {
        return new CropCatalogEntry
        {
            Id = dto.Id,
            Name = dto.Name,
            BinomialName = dto.BinomialName,
            IsPerennial = dto.IsPerennial,
            DaysToFirstHarvest = dto.DaysToFirstHarvest,
            DaysToLastHarvest = dto.DaysToLastHarvest,
            MinMonthsToBearFruit = dto.MinMonthsToBearFruit,
            MaxMonthsToBearFruit = dto.MaxMonthsToBearFruit,
            HarvestSeasonStart = dto.HarvestSeasonStart,
            HarvestSeasonEnd = dto.HarvestSeasonEnd,
            Description = dto.Description,
            WikipediaLink = dto.WikipediaLink,
            ImageLink = dto.ImageLink,
            SunRequirements = dto.SunRequirements,
            SowingMethod = dto.SowingMethod
        };
    }

    public static CropCatalogDto ToDto(this CropCatalogEntry entity)
    {
        return new CropCatalogDto(
            entity.Id,
            entity.Name,
            entity.BinomialName,
            entity.IsPerennial,
            entity.DaysToFirstHarvest,
            entity.DaysToLastHarvest,
            entity.MinMonthsToBearFruit,
            entity.MaxMonthsToBearFruit,
            entity.HarvestSeasonStart?.ToString("yyyy-MM-dd"),
            entity.HarvestSeasonEnd?.ToString("yyyy-MM-dd"),
            entity.Description,
            entity.WikipediaLink,
            entity.ImageLink,
            entity.SunRequirements,
            entity.SowingMethod
        );
    }
}