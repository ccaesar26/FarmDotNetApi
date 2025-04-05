using PlantedCropsService.Mapping.Extensions;
using PlantedCropsService.Models.Dtos;
using PlantedCropsService.Repositories.CropRepository;
using PlantedCropsService.Services.UnitOfWork;

namespace PlantedCropsService.Services.CropService;

public class CropService(ICropRepository cropRepository, IUnitOfWork unitOfWork) : ICropService
{
    private readonly ICropRepository _cropRepository =
        cropRepository ?? throw new ArgumentNullException(nameof(cropRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<CropDto?> GetCropByIdAsync(Guid id)
    {
        var crop = await _cropRepository.GetByIdAsync(id);
        return crop?.ToDto();
    }

    public async ValueTask<IEnumerable<CropDto>> GetAllCropsAsync()
    {
        var crops = await _cropRepository.GetAllAsync();
        return crops.Select(c => c.ToDto());
    }

    public async ValueTask<CropDto> AddCropAsync(CropCreateDto createDto)
    {
        var cropEntity = createDto.ToEntity();
        await _cropRepository.AddAsync(cropEntity);
        await _unitOfWork.SaveChangesAsync();
        return cropEntity.ToDto();
    }

    public async ValueTask UpdateCropAsync(CropUpdateDto updateDto)
    {
        var existingCrop = await _cropRepository.GetByIdAsync(updateDto.Id);
        if (existingCrop == null)
        {
            throw new KeyNotFoundException($"Crop with id {updateDto.Id} not found.");
        }

        // Update properties from DTO
        existingCrop.Name = updateDto.Name;
        existingCrop.BinomialName = updateDto.BinomialName;
        existingCrop.CultivatedVariety = updateDto.CultivatedVariety;
        existingCrop.ImageLink = updateDto.ImageLink;
        existingCrop.Perrenial = updateDto.Perrenial;
        existingCrop.ExpectedFirstHarvestDate = updateDto.ExpectedFirstHarvestDate;
        existingCrop.ExpectedLastHarvestDate = updateDto.ExpectedLastHarvestDate;
        existingCrop.FieldId = updateDto.FieldId;
        if (updateDto.Surface != null)
            existingCrop.Surface = CropMappingExtensions.ConvertGeoJsonToPolygon(updateDto.Surface);
        existingCrop.Area = updateDto.Area;
        existingCrop.CropCatalogId = updateDto.CropCatalogId;

        _cropRepository.Update(existingCrop);
        await _unitOfWork.SaveChangesAsync();
    }

    public async ValueTask DeleteCropAsync(Guid id)
    {
        var cropToDelete = await _cropRepository.GetByIdAsync(id);
        if (cropToDelete != null)
        {
            _cropRepository.Delete(cropToDelete);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}