using PlantedCropsService.Models.Dtos;

namespace PlantedCropsService.Services.GrowthStageEventService;

public interface IGrowthStageEventService
{
    ValueTask<GrowthStageEventDto?> GetGrowthStageEventByIdAsync(Guid id);
    ValueTask<IEnumerable<GrowthStageEventDto>> GetAllGrowthStageEventsAsync();
    ValueTask<GrowthStageEventDto> AddGrowthStageEventAsync(GrowthStageEventCreateDto createDto);
    ValueTask UpdateGrowthStageEventAsync(GrowthStageEventUpdateDto updateDto);
    ValueTask DeleteGrowthStageEventAsync(Guid id);
}