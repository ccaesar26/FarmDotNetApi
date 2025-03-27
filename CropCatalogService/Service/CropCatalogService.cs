using CropCatalogService.Extensions;
using CropCatalogService.Model.Dtos;
using CropCatalogService.Model.Entities;
using CropCatalogService.Repository;

namespace CropCatalogService.Service;

public class CropCatalogService(ICropCatalogRepository cropCatalogRepository) : ICropCatalogService
{
    public async ValueTask<IEnumerable<CropCatalogDto>> GetAllCropsAsync() =>
        (await cropCatalogRepository.GetAllCropsAsync()).Select(c => c.ToDto());

    public async ValueTask<CropCatalogDto?> GetCropByIdAsync(Guid id) =>
        (await cropCatalogRepository.GetCropByIdAsync(id))?.ToDto();

    public async ValueTask<bool> CropExistsAsync(Guid id) =>
        await cropCatalogRepository.CropExistsAsync(id);

    public async ValueTask<CropCatalogDto> AddCropAsync(CreateCropCatalogDto crop)
    {
        var cropEntity = crop.ToEntity();
        await cropCatalogRepository.AddCropAsync(cropEntity);
        return cropEntity.ToDto();
    }

    public async ValueTask UpdateCropAsync(UpdateCropCatalogDto crop) =>
        await cropCatalogRepository.UpdateCropAsync(crop.ToEntity());


    public async ValueTask DeleteCropAsync(Guid id) =>
        await cropCatalogRepository.DeleteCropAsync(id);
}