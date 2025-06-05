using CropIdService.Data;
using CropIdService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CropIdService.Repositories.IdEntryRepository;

public class IdEntryRepository(CropIdDbContext dbContext, ILogger<IdEntryRepository> logger) : IIdEntryRepository
{
    public async ValueTask<IdEntry> AddIdEntryAsync(IdEntry idEntry)
    {
        dbContext.IdEntries.Add(idEntry);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added IdEntry with ID: {IdEntryId}", idEntry.Id);
        return idEntry;
    }

    public async ValueTask<IdEntry?> GetIdEntryByIdAsync(Guid id)
    {
        var idEntry = await dbContext.IdEntries.FindAsync(id);
        if (idEntry == null)
        {
            logger.LogWarning("IdEntry with ID: {IdEntryId} not found", id);
            return null;
        }
        logger.LogInformation("Retrieved IdEntry with ID: {IdEntryId}", id);
        return idEntry;
    }

    public async ValueTask<IEnumerable<IdEntry>> GetAllIdEntriesByFarmIdAsync(Guid farmId)
    {
        var idEntries = await dbContext.IdEntries
            .Where(idEntry => idEntry.FarmId == farmId)
            .Include(idEntry => idEntry.Suggestions)
            .ToListAsync();
        logger.LogInformation("Retrieved {Count} IdEntries for Farm ID: {farmId}", idEntries.Count, farmId);
        return idEntries;
    }

    public async ValueTask<IdEntry?> UpdateIdEntryAsync(IdEntry idEntry)
    {
        var existingIdEntry = await dbContext.IdEntries.FindAsync(idEntry.Id);
        if (existingIdEntry == null)
        {
            logger.LogWarning("IdEntry with ID: {IdEntryId} not found for update", idEntry.Id);
            return null;
        }

        dbContext.Entry(existingIdEntry).CurrentValues.SetValues(idEntry);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Updated IdEntry with ID: {IdEntryId}", idEntry.Id);
        return existingIdEntry;
    }

    public async ValueTask<bool> DeleteIdEntryAsync(Guid id)
    {
        var idEntry = await dbContext.IdEntries.FindAsync(id);
        if (idEntry == null)
        {
            logger.LogWarning("IdEntry with ID: {IdEntryId} not found for deletion", id);
            return false;
        }

        dbContext.IdEntries.Remove(idEntry);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Deleted IdEntry with ID: {IdEntryId}", id);
        return true;
    }
}