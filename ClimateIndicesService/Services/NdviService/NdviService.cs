using System.Globalization;
using System.Text.Json;
using ClimateIndicesService.Models.Dtos;
using ClimateIndicesService.Models.VitoResponses;
using Microsoft.Extensions.Options;

namespace ClimateIndicesService.Services.NdviService;

public class NdviService(
    IHttpClientFactory httpClientFactory,
    IOptions<VitoWmsSettings> vitoSettings,
    // IWekeoTokenService wekeoTokenService, // Inject if using WEkEO HDA for time series
    // IOptions<WekeoApiSettings> wekeoSettings,  // Inject if using WEkEO HDA
    ILogger<NdviService> logger)
    : INdviService
{
    private readonly VitoWmsSettings _vitoSettings = vitoSettings.Value;
        // private readonly IWekeoTokenService _wekeoTokenService; // Needed if directly querying WEkEO HDA
        // private readonly WekeoApiSettings _wekeoSettings;      // Needed if directly querying WEkEO HDA

        // _wekeoTokenService = wekeoTokenService;
        // _wekeoSettings = wekeoSettings.Value;

        public Task<MapLayerInfo> GetMapLayerInfoAsync(CancellationToken cancellationToken = default)
        {
            // For a more dynamic approach, you could call VITO WMS GetCapabilities here
            // and parse it to get layer details, available times, styles, etc.
            var info = new MapLayerInfo
            {
                WmsBaseUrl = _vitoSettings.BaseUrl,
                LayerName = _vitoSettings.NdviLayerName
            };
            return Task.FromResult(info);
        }

        public async Task<IEnumerable<NdviDataPoint>> GetNdviTimeSeriesAsync(double lat, double lon, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var results = new List<NdviDataPoint>();
            var client = httpClientFactory.CreateClient("VitoWmsClient"); // Dedicated client for VITO

            logger.LogInformation("Fetching NDVI time series for Lat: {Lat}, Lon: {Lon} from {StartDate} to {EndDate}", lat, lon, startDate, endDate);

            // The NDVI product from HR-VPP is daily.
            for (DateTime currentDate = startDate.Date; currentDate <= endDate.Date; currentDate = currentDate.AddDays(1))
            {
                if (cancellationToken.IsCancellationRequested) break;

                // Construct WMS GetFeatureInfo URL
                double buffer = 0.0001; // Very small buffer for point query
                string bboxStr = string.Format(CultureInfo.InvariantCulture,
                    "{0},{1},{2},{3}",
                    lon - buffer, lat - buffer, lon + buffer, lat + buffer);
                
                string timeStr = currentDate.ToString("yyyy-MM-dd");

                // Note: VITO's HR-VPP WMTS/WMS uses EPSG:3857 by default for tiles.
                // For GetFeatureInfo, we might query in EPSG:4326 if supported, or project the point.
                // For simplicity, assuming EPSG:4326 is fine for BBOX in GetFeatureInfo,
                // or that the WMS handles it. Width/Height=1, I=0,J=0 means query the center of the BBOX.
                var requestUrl = $"{_vitoSettings.BaseUrl}" +
                                 $"?SERVICE=WMS&VERSION=1.1.1" + // Using 1.1.1 as it's very common
                                 $"&REQUEST=GetFeatureInfo" +
                                 $"&LAYERS={_vitoSettings.NdviLayerName}" +
                                 $"&QUERY_LAYERS={_vitoSettings.NdviLayerName}" +
                                 $"&BBOX={bboxStr}&SRS=EPSG:4326" + // Specify CRS of BBOX
                                 $"&WIDTH=1&HEIGHT=1&X=0&Y=0" +    // Pixel coords within the 1x1 image
                                 $"&INFO_FORMAT=application/json" + // CRITICAL: Request JSON
                                 $"&TIME={timeStr}" +
                                 $"&STYLES="; // Often empty or 'default'

                try
                {
                    logger.LogDebug("Requesting GetFeatureInfo: {RequestUrl}", requestUrl);
                    var response = await client.GetAsync(requestUrl, cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync(cancellationToken);
                        logger.LogTrace("GetFeatureInfo response for {Date}: {Content}", timeStr, content);
                        double? ndviValue = ParseNdviFromVitoResponse(content, timeStr);
                        results.Add(new NdviDataPoint { Date = currentDate, Value = ndviValue });
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                        logger.LogWarning("Failed to get NDVI for {Date} at Lat/Lon: {Lat}/{Lon}. Status: {StatusCode}. Response: {ErrorContent}. URL: {RequestUrl}",
                                           timeStr, lat, lon, response.StatusCode, errorContent.Length > 500 ? errorContent.Substring(0,500) : errorContent, requestUrl);
                        results.Add(new NdviDataPoint { Date = currentDate, Value = null });
                    }
                }
                catch (HttpRequestException httpEx)
                {
                     logger.LogError(httpEx, "HTTP Request Exception for GetFeatureInfo on {Date} at {Lat},{Lon}. URL: {RequestUrl}", timeStr, lat, lon, requestUrl);
                    results.Add(new NdviDataPoint { Date = currentDate, Value = null });
                }
                catch (JsonException jsonEx)
                {
                    logger.LogError(jsonEx, "JSON Parsing Exception for GetFeatureInfo on {Date} at {Lat},{Lon}. URL: {RequestUrl}", timeStr, lat, lon, requestUrl);
                    results.Add(new NdviDataPoint { Date = currentDate, Value = null });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Generic Exception for GetFeatureInfo on {Date} at {Lat},{Lon}. URL: {RequestUrl}", timeStr, lat, lon, requestUrl);
                    results.Add(new NdviDataPoint { Date = currentDate, Value = null });
                }
                await Task.Delay(150, cancellationToken); // Be respectful to the WMS server
            }
            logger.LogInformation("Finished fetching NDVI time series. Got {Count} points.", results.Count);
            return results;
        }

        // CRITICAL: VERIFY AND ADJUST THIS PARSER BASED ON ACTUAL VITO WMS RESPONSE
        private double? ParseNdviFromVitoResponse(string responseContent, string dateForLog)
        {
            if (string.IsNullOrWhiteSpace(responseContent) || responseContent.Trim().Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                 logger.LogDebug("GetFeatureInfo for {Date} returned empty or 'none' content.", dateForLog);
                return null;
            }

            try
            {
                // Attempt to deserialize as our expected GeoJSON structure
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var parsedResponse = JsonSerializer.Deserialize<VitoGetFeatureInfoResponse>(responseContent, options);

                if (parsedResponse?.Features != null && parsedResponse.Features.Any())
                {
                    var featureProperties = parsedResponse.Features[0].Properties;
                    if (featureProperties?.GrayIndex != null)
                    {
                        // HR-VPP NDVI products are often scaled: value * 10000
                        // So, raw NDVI = GrayIndex / 10000.0
                        // IMPORTANT: Verify this scaling from the product documentation or VITO's WMS capabilities/examples!
                        // If GrayIndex is -9999, -32768, or similar, it might be a no-data value.
                        if (featureProperties.GrayIndex < -10000 || featureProperties.GrayIndex > 10000) // Example range check for no-data
                        {
                            logger.LogDebug("GetFeatureInfo for {Date} returned GrayIndex {GrayIndex} which might be a no-data value.", dateForLog, featureProperties.GrayIndex);
                            return null;
                        }
                        return featureProperties.GrayIndex.Value / 10000.0;
                    }
                    // Add other property checks if 'GRAY_INDEX' is not the one
                    // else if (featureProperties?.Value != null) return featureProperties.Value;
                }
                logger.LogWarning("Could not find NDVI value in GetFeatureInfo response for {Date}. Content: {Content}", dateForLog, responseContent.Length > 200 ? responseContent.Substring(0,200): responseContent);
                return null;
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Failed to parse VITO GetFeatureInfo JSON response for {Date}: {ResponseContent}", dateForLog, responseContent);
                return null;
            }
        }
    }