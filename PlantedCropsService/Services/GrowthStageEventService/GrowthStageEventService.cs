using PlantedCropsService.Mapping.Extensions;
using PlantedCropsService.Models.Dtos;
using PlantedCropsService.Repositories.GrowthStageEventRepository;
using PlantedCropsService.Services.UnitOfWork;

namespace PlantedCropsService.Services.GrowthStageEventService;

public class GrowthStageEventService(IGrowthStageEventRepository growthStageEventRepository, IUnitOfWork unitOfWork)
    : IGrowthStageEventService
{
    private readonly IGrowthStageEventRepository _growthStageEventRepository = growthStageEventRepository ??
                                                                               throw new ArgumentNullException(
                                                                                   nameof(growthStageEventRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<GrowthStageEventDto?> GetGrowthStageEventByIdAsync(Guid id)
    {
        var growthStageEvent = await _growthStageEventRepository.GetByIdAsync(id);
        return growthStageEvent?.ToDto();
    }

    public async ValueTask<IEnumerable<GrowthStageEventDto>> GetAllGrowthStageEventsAsync()
    {
        var events = await _growthStageEventRepository.GetAllAsync();
        return events.Select(e => e.ToDto());
    }

    public async ValueTask<GrowthStageEventDto> AddGrowthStageEventAsync(GrowthStageEventCreateDto createDto)
    {
        var eventEntity = createDto.ToEntity();
        await _growthStageEventRepository.AddAsync(eventEntity);
        await _unitOfWork.SaveChangesAsync();
        return eventEntity.ToDto();
    }

    public async ValueTask UpdateGrowthStageEventAsync(GrowthStageEventUpdateDto updateDto)
    {
        var existingEvent = await _growthStageEventRepository.GetByIdAsync(updateDto.Id);
        if (existingEvent == null)
        {
            throw new KeyNotFoundException($"GrowthStageEvent with id {updateDto.Id} not found.");
        }

        existingEvent.CropId = updateDto.CropId;
        existingEvent.Stage = updateDto.Stage;
        existingEvent.Timestamp = updateDto.Timestamp;
        existingEvent.RecordedByUserId = updateDto.RecordedByUserId;
        existingEvent.RecordedByUserName = updateDto.RecordedByUserName;

        _growthStageEventRepository.Update(existingEvent);
        await _unitOfWork.SaveChangesAsync();
    }

    public async ValueTask DeleteGrowthStageEventAsync(Guid id)
    {
        var eventToDelete = await _growthStageEventRepository.GetByIdAsync(id);
        if (eventToDelete != null)
        {
            _growthStageEventRepository.Delete(eventToDelete);
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"GrowthStageEvent with id {id} not found.");
        }
    }
}