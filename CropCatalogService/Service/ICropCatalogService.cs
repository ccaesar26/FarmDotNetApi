using CropCatalogService.Model.Dtos;
using CropCatalogService.Model.Entities;

namespace CropCatalogService.Service;

public interface ICropCatalogService
{
    ValueTask<IEnumerable<CropCatalogDto>> GetAllCropsAsync();
    ValueTask<CropCatalogDto?> GetCropByIdAsync(Guid id);
    ValueTask<bool> CropExistsAsync(Guid id);
    ValueTask<CropCatalogDto> AddCropAsync(CreateCropCatalogDto crop);
    ValueTask UpdateCropAsync(UpdateCropCatalogDto crop);
    ValueTask DeleteCropAsync(Guid id);
}