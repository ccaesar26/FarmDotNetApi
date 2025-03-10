namespace ClimateIndicesService.Repositories;

public interface IDroughtRasterRepository
{
    ValueTask<int> GetDroughtValueAsync(byte[] rasterData, double lon, double lat);
}