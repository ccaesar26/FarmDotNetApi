using PlantedCropsService.Data;
using PlantedCropsService.Models.Entities;
using PlantedCropsService.Repositories.GenericRepository;

namespace PlantedCropsService.Repositories.FertilizerEventRepository;

public class FertilizerEventRepository(CropsDbContext context)
    : GenericRepository<FertilizerEvent>(context), IFertilizerEventRepository;