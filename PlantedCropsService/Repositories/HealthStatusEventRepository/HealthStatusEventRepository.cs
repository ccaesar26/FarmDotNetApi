using PlantedCropsService.Data;
using PlantedCropsService.Models.Entities;
using PlantedCropsService.Repositories.GenericRepository;

namespace PlantedCropsService.Repositories.HealthStatusEventRepository;

public class HealthStatusEventRepository(CropsDbContext context)
    : GenericRepository<HealthStatusEvent>(context), IHealthStatusEventRepository
{
    
}