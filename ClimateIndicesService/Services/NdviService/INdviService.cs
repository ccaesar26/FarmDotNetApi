using ClimateIndicesService.Models.Dtos;

namespace ClimateIndicesService.Services.NdviService;

public interface INdviService
{
    Task<MapLayerInfo> GetMapLayerInfoAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<NdviDataPoint>> GetNdviTimeSeriesAsync(double lat, double lon, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}