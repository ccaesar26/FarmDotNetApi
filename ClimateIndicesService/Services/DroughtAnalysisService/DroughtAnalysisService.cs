using ClimateIndicesService.Models;
using ClimateIndicesService.Repositories;

namespace ClimateIndicesService.Services.DroughtAnalysisService;

public class DroughtAnalysisService(
    ILogger<DroughtAnalysisService> logger,
    IDroughtRasterRepository rasterRepository
) : IDroughtAnalysisService
{
    public async ValueTask<DroughtLevelInfo> ComputeDroughtLevelForCoordinates(byte[] raster, double lon, double lat)
    {
        try
        {
            var droughtValue = await rasterRepository.GetDroughtValueAsync(raster, lon, lat);
            return CreateDroughtInfo(droughtValue);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error computing drought level for coordinates: Lon={Lon}, Lat={Lat}", lon, lat);
            throw; // Re-throw the exception after logging
        }
    }

    private static readonly Dictionary<int, string> DroughtLevelMeanings = new()
    {
        { 0, "No Drought" },
        { 1, "Watch" },
        { 2, "Warning" },
        { 3, "Alert" },
        { 4, "Recovery" },
        { 5, "Temporary Soil Moisture Recovery" },
        { 6, "Temporary Vegetation Recovery" }
    };

    private static readonly Dictionary<int, string> FarmerFriendlyDescriptions = new()
    {
        {
            0,
            "Normal conditions. No drought detected."
        },
        {
            1,
            "Precipitation is lower than usual. Be watchful for potential drought conditions. Consider water conservation measures."
        },
        {
            2,
            "Soil moisture is significantly low. Drought conditions are likely developing. Monitor your crops closely and optimize water usage."
        },
        {
            3,
            "Vegetation growth is negatively impacted by drought. Drought conditions are severe. Implement drought mitigation strategies and seek expert advice."
        },
        {
            4,
            "Conditions are returning to normal after a drought. Recovery is underway."
        },
        {
            5,
            "Soil moisture is recovering, but drought conditions are not fully over. Continue monitoring."
        },
        {
            6,
            "Vegetation is showing signs of recovery, but drought conditions are not fully over. Continue monitoring."
        }
    };

    private static DroughtLevelInfo CreateDroughtInfo(int droughtValue)
    {
        return new DroughtLevelInfo(
            droughtValue,
            DroughtLevelMeanings.GetValueOrDefault(droughtValue, "Unknown"),
            FarmerFriendlyDescriptions.GetValueOrDefault(droughtValue, "Unknown")
        );
    }
}