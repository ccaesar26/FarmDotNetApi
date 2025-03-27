using CropCatalogService.Data;
using CropCatalogService.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CropCatalogService.Repository;

public class CropCatalogRepository(CropCatalogDbContext context) : ICropCatalogRepository
{
    public async ValueTask<IEnumerable<CropCatalogEntry>> GetAllCropsAsync()
    {
        // Use AsNoTracking for read-only queries
        return await context.CropCatalogEntries.AsNoTracking().ToListAsync();
    }

    public async ValueTask<CropCatalogEntry?> GetCropByIdAsync(Guid id)
    {
        // Use AsNoTracking for read-only queries
        return await context.CropCatalogEntries.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async ValueTask<bool> CropExistsAsync(Guid id)
    {
        // Use AsNoTracking for read-only queries
        return await context.CropCatalogEntries.AsNoTracking().AnyAsync(c => c.Id == id);
    }

    public async ValueTask AddCropAsync(CropCatalogEntry crop)
    {
        // Use AsNoTracking for read-only queries
        await context.CropCatalogEntries.AddAsync(crop);
        await context.SaveChangesAsync();
    }

    public async ValueTask UpdateCropAsync(CropCatalogEntry crop)
    {
        // Use AsNoTracking for read-only queries
        context.CropCatalogEntries.Update(crop);
        await context.SaveChangesAsync();
    }

    public async ValueTask DeleteCropAsync(Guid id)
    {
        var crop = await context.CropCatalogEntries.FindAsync(id);
        if (crop != null)
        {
            context.CropCatalogEntries.Remove(crop);
            await context.SaveChangesAsync();
        }
    }
}