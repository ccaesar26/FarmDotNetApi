using ClimateIndicesService.Data;
using ClimateIndicesService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClimateIndicesService.Repositories;

public class DroughtRecordRepository(
    ClimateIndicesDbContext dbContext
) : IDroughtRecordRepository
{
    public async ValueTask<DroughtRecord> AddDroughtRecordAsync(byte[] rasterData, DateTime date)
    {
        var record = new DroughtRecord
        {
            Date = date.Kind == DateTimeKind.Utc ? date : date.ToUniversalTime(), // Ensure UTC
            RasterData = rasterData
        };

        record = dbContext.DroughtRecords.Add(record).Entity;
        await dbContext.SaveChangesAsync();
    
        return record;
    }

    public async ValueTask<DroughtRecord?> GetDroughtRecordAsync(DateTime date)
    {
        date = date.ToUniversalTime();
        return await dbContext.DroughtRecords.FirstOrDefaultAsync(r => r.Date.Date == date.Date);
    }

    public async ValueTask<IEnumerable<DroughtRecord>> GetDroughtRecordsAsync()
    {
        return await dbContext.DroughtRecords.ToListAsync();
    }
}