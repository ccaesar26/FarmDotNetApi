using PlantedCropsService.Data;
using PlantedCropsService.Models.Entities;
using PlantedCropsService.Repositories.GenericRepository;

namespace PlantedCropsService.Repositories.GrowthStageEventRepository;

public class GrowthStageEventRepository(CropsDbContext context)
    : GenericRepository<GrowthStageEvent>(context), IGrowthStageEventRepository
{
    
}