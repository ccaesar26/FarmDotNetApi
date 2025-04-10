using Microsoft.EntityFrameworkCore;
using PlantedCropsService.Data;
using PlantedCropsService.Models.Entities;
using PlantedCropsService.Repositories.GenericRepository;

namespace PlantedCropsService.Repositories.CropRepository;

public class CropRepository(CropsDbContext context) : GenericRepository<Crop>(context), ICropRepository
{
    private readonly CropsDbContext _context = context;

    // Add any crop-specific repository methods implementation here if needed
    // Example:
    public async Task<IEnumerable<Crop?>> GetCropsByFieldIdAsync(Guid fieldId)
    {
        return await _context.Crops.Where(c => c.FieldId == fieldId).ToListAsync();
    }

    public async ValueTask<IEnumerable<Crop>> GetAllByFarmIdAsync(Guid farmId)
    {
        return await _context.Crops.Where(c => c.FarmId == farmId).ToListAsync();
    }
}