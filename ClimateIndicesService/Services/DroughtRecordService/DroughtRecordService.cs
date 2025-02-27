using ClimateIndicesService.ExternalClients.EdoApiClient;
using ClimateIndicesService.Models.Entities;
using ClimateIndicesService.Repositories;

namespace ClimateIndicesService.Services.DroughtRecordService;

public class DroughtRecordService(
    IDroughtRecordRepository droughtRecordRepository,
    IEdoApiClient edoApiClient
) : IDroughtRecordService
{
    public async ValueTask<DroughtRecord> AddDroughtRecordAsync(byte[] rasterData, DateTime date)
    {
        return await droughtRecordRepository.AddDroughtRecordAsync(rasterData, date);
    }

    public async ValueTask<DroughtRecord> GetDroughtRecordAsync(DateTime date)
    {
        var dr = await droughtRecordRepository.GetDroughtRecordAsync(date);

        if (dr is not null)
        {
            return dr;
        }

        var stream = await edoApiClient.GetDroughtDataAsync(null);
        using var rasterDataMemoryStream = new MemoryStream();
        await stream.CopyToAsync(rasterDataMemoryStream);
        return await droughtRecordRepository.AddDroughtRecordAsync(rasterDataMemoryStream.ToArray(), date);
    }

    public async ValueTask<IEnumerable<DroughtRecord>> GetDroughtRecordsAsync()
    {
        return await droughtRecordRepository.GetDroughtRecordsAsync();
    }
}