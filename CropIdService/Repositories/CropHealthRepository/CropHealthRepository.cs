using System.Net.Http.Headers;
using System.Text.Json;
using CropIdService.Models.CropHealthApi;

namespace CropIdService.Repositories.CropHealthRepository;

public class CropHealthRepository : ICropHealthRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CropHealthRepository> _logger;

    // JsonSerializerOptions for case-insensitive property name matching if needed
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Will serialize Images as "images", Latitude as "latitude" etc.
        WriteIndented = true, // For logging if you're also manually serializing
    };

    public CropHealthRepository(
        HttpClient httpClient, // Inject HttpClient using IHttpClientFactory
        IConfiguration configuration,
        ILogger<CropHealthRepository> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        var apiBaseUrl = configuration["CropHealthApiBaseUrl"]
                         ?? throw new InvalidOperationException("CropHealthApiBaseUrl not found in configuration.");
        var apiKey = configuration["KindWiseApiKey"]
                     ?? throw new InvalidOperationException("KindWiseApiKey not found in configuration.");

        // Configure HttpClient defaults if not already configured via IHttpClientFactory
        _httpClient.BaseAddress = new Uri(apiBaseUrl); // Set the base URL
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Api-Key", apiKey);
    }

    public async ValueTask<IdentificationResponse?> IdentifyAsync(
        List<string> base64Images,
        double? latitude = null,
        double? longitude = null,
        string? datetime = null
    )
    {
        if (base64Images.Count == 0)
        {
            _logger.LogWarning("No images provided for identification.");
            return null;
        }

        var requestPayload = new CropHealthApiRequest(base64Images, latitude, longitude, datetime);
        
        _logger.LogInformation("Sending identification request to Crop Health API with {ImageCount} images.", base64Images.Count);
        // Log the request payload for debugging (be careful with sensitive data)
        _logger.LogCritical("Request Payload: {Payload}", JsonSerializer.Serialize(requestPayload, JsonSerializerOptions));

        try
        {
            // The BaseAddress is already set, so just use the relative path or an empty string if BaseAddress is the full endpoint
            // Assuming _apiBaseUrl from config is the full endpoint URL including "/identification"
            // If _apiBaseUrl is just "https://crop.kindwise.com/api/v1", then the path would be "identification"
            // For this example, let's assume _apiBaseUrl IS the full identification endpoint URL.
            // If not, adjust the Uri for PostAsJsonAsync accordingly.
            
            // If _apiBaseUrl is "https://crop.kindwise.com/api/v1/identification"
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", requestPayload, JsonSerializerOptions);
            // OR if _apiBaseUrl is "https://crop.kindwise.com/api/v1"
            // HttpResponseMessage response = await _httpClient.PostAsJsonAsync("identification", requestPayload, _jsonSerializerOptions);

            if (response.IsSuccessStatusCode)
            {
                var identificationResponse = await response.Content.ReadFromJsonAsync<IdentificationResponse>(JsonSerializerOptions);
                _logger.LogInformation("Successfully received identification from Crop Health API.");
                return identificationResponse;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error from Crop Health API: {StatusCode} - {ReasonPhrase}. Content: {ErrorContent}",
                    response.StatusCode, response.ReasonPhrase, errorContent);
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error when calling Crop Health API.");
            return null;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON deserialization error from Crop Health API response.");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred when calling Crop Health API.");
            return null;
        }
    }
}