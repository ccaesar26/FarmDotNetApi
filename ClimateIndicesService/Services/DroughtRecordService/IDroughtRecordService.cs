using ClimateIndicesService.Models.Entities;

namespace ClimateIndicesService.Services.DroughtRecordService;

public interface IDroughtRecordService
{
    ValueTask<DroughtRecord> AddDroughtRecordAsync(byte[] rasterData, DateTime date);
    
    ValueTask<DroughtRecord> GetDroughtRecordAsync(DateTime date);
    
    ValueTask<IEnumerable<DroughtRecord>> GetDroughtRecordsAsync();
}