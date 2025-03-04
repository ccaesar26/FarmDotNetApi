using ClimateIndicesService.Data;
using ClimateIndicesService.Models;
using MaxRev.Gdal.Core;
using OSGeo.GDAL;


namespace ClimateIndicesService.Services.DroughtAnalysisService;

public class DroughtAnalysisService : IDroughtAnalysisService
{
    private readonly ILogger<DroughtAnalysisService> _logger;
    private readonly ClimateIndicesDbContext _dbContext;

    public DroughtAnalysisService(ILogger<DroughtAnalysisService> logger, ClimateIndicesDbContext dbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        try
        {
            GdalBase.ConfigureAll();
            Console.WriteLine($"GDAL Version: {Gdal.VersionInfo("")}"); // Good for verification
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GDAL Initialization Failed: {ex.Message}");
            // Handle the error appropriately (e.g., log it, display an error message, exit the application).
        }
    }

    public async ValueTask<DroughtLevelInfo> ComputeDroughtLevelForCoordinates(byte[] raster, double lon, double lat)
    {
        var tempFilePath = Path.GetTempFileName() + ".tif";
        await File.WriteAllBytesAsync(tempFilePath, raster);

        // Open with GDAL
        using var dataset = Gdal.Open(tempFilePath, Access.GA_ReadOnly);
        if (dataset == null) throw new Exception("Failed to open GeoTIFF.");

        // Convert lon/lat to pixel coordinates
        var geoTransform = new double[6];
        dataset.GetGeoTransform(geoTransform);
        var (x, y) = ConvertGeoToPixel(lon, lat, geoTransform);

        // Read the pixel value
        var band = dataset.GetRasterBand(1);
        var buffer = new int[1];
        band.ReadRaster(x, y, 1, 1, buffer, 1, 1, 0, 0);
        var droughtValue = buffer[0];

        return CreateDroughtInfo(droughtValue);
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

    private static (int, int) ConvertGeoToPixel(double lon, double lat, double[] geoTransform)
    {
        var x = (int)((lon - geoTransform[0]) / geoTransform[1]);
        var y = (int)((lat - geoTransform[3]) / geoTransform[5]);
        return (x, y);
    }
}