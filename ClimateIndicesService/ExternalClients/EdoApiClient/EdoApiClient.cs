namespace ClimateIndicesService.ExternalClients.EdoApiClient;

public class EdoApiClient(
    HttpClient httpClient,
    IConfiguration configuration,
    ILogger<EdoApiClient> logger
) : IEdoApiClient
{
    private readonly string _edoBaseUrl = configuration["EdoApiBaseUrl"] ??
                                          throw new InvalidOperationException("EdoApiBaseUrl is not configured.");

    public async ValueTask<Stream> GetDroughtDataAsync(DateTime? time)
    {
        var fullUrl = CreateRequestString(time);
        logger.LogInformation($"Fetching drought data from: {fullUrl}");

        try
        {
            var response = await httpClient.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"EDO API request failed with status code: {response.StatusCode}");
                throw new HttpRequestException($"EDO API request failed with status code: {response.StatusCode}");
            }

            var contentStream = await response.Content.ReadAsStreamAsync();
            return contentStream;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError($"Error fetching drought data from EDO API: {ex.Message}");
            throw; // Re-throw the exception to be handled by the controller
        }
    }

    private string CreateRequestString(DateTime? time)
    {
        var parameters = new[]
        {
            "map=DO_WCS",
            "SERVICE=WCS",
            "VERSION=2.0.0",
            "REQUEST=GetCoverage",
            "coverageID=cdiad", // CDI Layer
            "CRS=EPSG:4326", // Coordinate Reference System
            "format=GEOTIFF"
        };

        if (time.HasValue)
        {
            parameters = parameters.Append($"TIME={time.Value:yyyy-MM-dd}").ToArray();
        }

        return $"{_edoBaseUrl}?{string.Join("&", parameters)}";
    }
}