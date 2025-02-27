using ClimateIndicesService.Models;

namespace ClimateIndicesService.Services.DroughtAnalysisService;

public interface IDroughtAnalysisService
{
    ValueTask<DroughtLevelInfo> ComputeDroughtLevelForCoordinates(byte[] raster, double lon, double lat);
}