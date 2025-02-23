using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using WeatherService.Extensions;
using WeatherService.Models.Dtos;

namespace WeatherService.Services.WeatherService;

public class WeatherService(
    HttpClient httpClient,
    IMemoryCache cache,
    IConfiguration configuration
) : IWeatherService
{
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
    
    public async ValueTask<FarmWeatherDto?> GetWeatherAsync(string city)
    {
        if (cache.TryGetValue(city, out FarmWeatherDto? weather))
        {
            return weather;
        }

        var apiKey = configuration["OpenWeatherMap:ApiKey"];
        var apiUrl = configuration["OpenWeatherMap:ApiUrl"];
        
        var url = apiUrl + $"weather?q={city}&appid={apiKey}&units=metric";
        
        weather = await GetWeatherFromUrlAsync(url);
        if (weather is null)
        {
            return null;
        }
        
        cache.Set(city, weather, _cacheDuration);
        return weather;
    }
    
    public async ValueTask<FarmWeatherDto?> GetWeatherAsync(double latitude, double longitude)
    {
        if (cache.TryGetValue($"{latitude},{longitude}", out FarmWeatherDto? weather))
        {
            return weather;
        }

        var apiKey = configuration["OpenWeatherMap:ApiKey"];
        var apiUrl = configuration["OpenWeatherMap:ApiUrl"];
        
        var url = apiUrl + $"weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";
        
        weather = await GetWeatherFromUrlAsync(url);
        if (weather is null)
        {
            return null;
        }
        
        cache.Set($"{latitude},{longitude}", weather, _cacheDuration);
        return weather;
    }
    
    private async ValueTask<FarmWeatherDto?> GetWeatherFromUrlAsync(string url)
    {
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<WeatherData>(content);
        return weatherData?.ToFarmWeatherDto();
    }
}