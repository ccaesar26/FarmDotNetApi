using System.Globalization;
using ClimateIndicesService.Models.Dtos;
using Microsoft.Extensions.Options;

namespace ClimateIndicesService.Services.NdviMapService;

public class NdviMapService : INdviMapService
{
    private readonly VitoWmsNdviSettings _wmsSettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<NdviMapService> _logger;
     // If you need WEkEO HDA for more precise date checking:
    // private readonly IWekeoTokenService _wekeoTokenService;
    // private readonly WekeoApiSettings _wekeoApiSettings;


    public NdviMapService(IOptions<VitoWmsNdviSettings> wmsSettings,
                          IHttpClientFactory httpClientFactory,
                          ILogger<NdviMapService> logger
                          /*, IWekeoTokenService wekeoTokenService, IOptions<WekeoApiSettings> wekeoApiSettings */)
    {
        _wmsSettings = wmsSettings.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        // _wekeoTokenService = wekeoTokenService;
        // _wekeoApiSettings = wekeoApiSettings.Value;
    }

    public async Task<NdviMapLayerDetails> GetMapLayerDetailsAsync(NdviMapInfoRequest request)
    {
        // Basic information is static from config
        var details = new NdviMapLayerDetails
        {
            WmsBaseUrl = _wmsSettings.BaseUrl,
            LayerName = _wmsSettings.LayerName,
            Style = _wmsSettings.DefaultStyle,
            Format = _wmsSettings.ImageFormat,
            Crs = _wmsSettings.Srs,
            InitialBoundingBox = new List<double> { request.MinLongitude, request.MinLatitude, request.MaxLongitude, request.MaxLatitude }
        };

        // Parse overall service availability dates
        DateTime overallServiceStart = DateTime.Parse(_wmsSettings.ServiceOverallStartDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        DateTime overallServiceEnd = DateTime.Parse(_wmsSettings.ServiceOverallEndDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

        // Clamp requested dates to service availability
        DateTime clampedStartDate = request.StartDate < overallServiceStart ? overallServiceStart : request.StartDate;
        DateTime clampedEndDate = request.EndDate > overallServiceEnd ? overallServiceEnd : request.EndDate;

        if (clampedStartDate > clampedEndDate)
        {
            _logger.LogInformation("Requested date range is outside the service's overall availability or invalid.");
            details.AvailableDates = new List<string>();
            details.MinAvailableDateInRequestRange = null;
            details.MaxAvailableDateInRequestRange = null;
            return details;
        }
        
        details.MinAvailableDateInRequestRange = clampedStartDate.ToString("yyyy-MM-dd");
        details.MaxAvailableDateInRequestRange = clampedEndDate.ToString("yyyy-MM-dd");

        // For NDVI, it's often a daily product. We can generate daily dates within the clamped range.
        // A more advanced method would query WEkEO HDA for actual data files within the BBOX and time range
        // to confirm which specific dates have imagery, but that's more complex and slower.
        // For map display, providing daily steps for the client to try is usually sufficient.
        var availableDatesInRange = new List<string>();
        for (DateTime date = clampedStartDate; date <= clampedEndDate; date = date.AddDays(1))
        {
            availableDatesInRange.Add(date.ToString("yyyy-MM-dd"));
        }
        details.AvailableDates = availableDatesInRange;

        // Optional: Call VITO WMS GetCapabilities to get the *actual* time dimension
        // This is more robust but adds an external HTTP call.
        // await PopulateTimeDimensionFromGetCapabilities(details);

        return details;
    }

     // Optional: More robust time dimension population
    private async Task PopulateTimeDimensionFromGetCapabilities(NdviMapLayerDetails details)
    {
        var client = _httpClientFactory.CreateClient();
        var getCapsUrl = $"{_wmsSettings.BaseUrl}?SERVICE=WMS&VERSION=1.3.0&REQUEST=GetCapabilities"; // or 1.1.1
        try
        {
            var response = await client.GetStringAsync(getCapsUrl);
            // TODO: Parse XML response to find the <Dimension name="time"> for the specific layer
            // This is non-trivial XML parsing.
            // Example: XPath: /WMS_Capabilities/Capability/Layer/Layer[Name='CLMS_HRVPP_VI_NDVI_10M']/Dimension[@name='time']
            // The content of this tag will list individual dates or a start/end/periodicity.
            // If it's a list, you'd intersect it with your clampedStartDate/clampedEndDate.
            _logger.LogInformation("Successfully fetched GetCapabilities. XML parsing needed to extract time dimension.");
            // For now, we'll stick to daily generation.
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch or parse WMS GetCapabilities for time dimension.");
            // Fallback to daily generation if GetCaps fails (already done)
        }
    }
}