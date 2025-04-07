using PlantedCropsService.Models.Dtos;
using PlantedCropsService.Models.Entities;

namespace PlantedCropsService.Services.CropService;

public interface ICropService
{
    ValueTask<CropDto?> GetCropByIdAsync(Guid id);
    ValueTask<IEnumerable<CropDto>> GetAllCropsAsync(Guid farmId);
    ValueTask<CropDto> AddCropAsync(CropCreateDto createDto, Guid farmId);
    ValueTask UpdateCropAsync(CropUpdateDto updateDto);
    ValueTask DeleteCropAsync(Guid id);
}