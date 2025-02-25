using System.Text.Json;
using FieldsService.Models.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace FieldsService.Services.GeocodingService;

public class GeocodingService(
    HttpClient httpClient,
    IMemoryCache cache,
    IConfiguration configuration
) : IGeocodingService
{
    private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(1);
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
    
    public async ValueTask<GeocodingResult?> GetCityByCoordinatesAsync(double latitude, double longitude)
    {
        // var cacheKey = $"city-{latitude}-{longitude}";
        // if (cache.TryGetValue(cacheKey, out string? city))
        // {
        //     return city;
        // }
        
        var apiKey = configuration["OpenWeatherMap:ApiKey"];
        var apiBaseUrl = configuration["OpenWeatherMap:ApiUrl"];
        var url = $"{apiBaseUrl}reverse?lat={latitude}&lon={longitude}&appid={apiKey}";
        
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<GeocodingResult>>(json, _jsonOptions);

        // Not the first city, but the second one if exists, else the first one.
        // city = data?.Count > 1 ? data[1].Name : data?[0].Name;
        // if (!string.IsNullOrEmpty(city))
        // {
        //     cache.Set(cacheKey, city, _cacheDuration);
        // }
        
        return data?[0];
    }
}