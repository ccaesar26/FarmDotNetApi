// ClimateIndicesService.Repositories.GdalDroughtRasterRepository

using OSGeo.GDAL;

namespace ClimateIndicesService.Repositories;

public class GdalDroughtRasterRepository(ILogger<GdalDroughtRasterRepository> logger) : IDroughtRasterRepository
{

    public async ValueTask<int> GetDroughtValueAsync(byte[] rasterData, double lon, double lat)
    {
        var tempFilePath = Path.GetTempFileName() + ".tif";
        try
        {
            await File.WriteAllBytesAsync(tempFilePath, rasterData);

            using var dataset = Gdal.Open(tempFilePath, Access.GA_ReadOnly);
            if (dataset == null)
            {
                logger.LogError("Failed to open GeoTIFF at path: {TempFilePath}", tempFilePath); // Log path
                throw new Exception("Failed to open GeoTIFF.");
            }

            var geoTransform = new double[6];
            dataset.GetGeoTransform(geoTransform);
            var (x, y) = ConvertGeoToPixel(lon, lat, geoTransform);

            var band = dataset.GetRasterBand(1);
            var buffer = new int[1];
            band.ReadRaster(x, y, 1, 1, buffer, 1, 1, 0, 0);

            return buffer[0];
        }
        finally
        {
            try
            {
                File.Delete(tempFilePath); // Clean up the temporary file, even if there are exceptions
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to delete temporary file: {TempFilePath}",
                    tempFilePath); //Log if can not delete
            }
        }
    }

    private static (int, int) ConvertGeoToPixel(double lon, double lat, double[] geoTransform)
    {
        var x = (int)((lon - geoTransform[0]) / geoTransform[1]);
        var y = (int)((lat - geoTransform[3]) / geoTransform[5]);
        return (x, y);
    }
}