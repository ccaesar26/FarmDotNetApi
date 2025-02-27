using ClimateIndicesService.Models.Entities;

namespace ClimateIndicesService.Repositories;

public interface IDroughtRecordRepository
{
    ValueTask<DroughtRecord> AddDroughtRecordAsync(byte[] rasterData, DateTime date);
    
    ValueTask<DroughtRecord?> GetDroughtRecordAsync(DateTime date);
    
    ValueTask<IEnumerable<DroughtRecord>> GetDroughtRecordsAsync();
}