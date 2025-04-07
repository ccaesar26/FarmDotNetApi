using PlantedCropsService.Models.Entities;
using PlantedCropsService.Repositories.GenericRepository;

namespace PlantedCropsService.Repositories.CropRepository;

public interface ICropRepository : IGenericRepository<Crop>
{
    ValueTask<IEnumerable<Crop>> GetAllByFarmIdAsync(Guid farmId);
}