using CropCatalogService.Model.Entities;

namespace CropCatalogService.Repository;

public interface ICropCatalogRepository
{
    ValueTask<IEnumerable<CropCatalogEntry>> GetAllCropsAsync();
    ValueTask<CropCatalogEntry?> GetCropByIdAsync(Guid id);
    ValueTask<bool> CropExistsAsync(Guid id);
    ValueTask AddCropAsync(CropCatalogEntry crop);
    ValueTask UpdateCropAsync(CropCatalogEntry crop);
    ValueTask DeleteCropAsync(Guid id);
}