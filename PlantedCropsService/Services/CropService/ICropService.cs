using PlantedCropsService.Models.Dtos;
using PlantedCropsService.Models.Entities;

namespace PlantedCropsService.Services.CropService;

public interface ICropService
{
    ValueTask<CropDto?> GetCropByIdAsync(Guid id);
    ValueTask<IEnumerable<CropDto>> GetAllCropsAsync();
    ValueTask<CropDto> AddCropAsync(CropCreateDto createDto);
    ValueTask UpdateCropAsync(CropUpdateDto updateDto);
    ValueTask DeleteCropAsync(Guid id);
}