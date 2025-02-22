using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using WeatherService.Extensions;
using WeatherService.Models.Dtos;

namespace WeatherService.Services;

public class WeatherService(
    HttpClient httpClient,
    IMemoryCache cache,
    IConfiguration configuration
) : IWeatherService
{
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);
    
    public async ValueTask<FarmWeatherDto?> GetWeatherAsync(string city)
    {
        if (cache.TryGetValue(city, out FarmWeatherDto? weather))
        {
            return weather;
        }

        var apiKey = configuration["OpenWeatherMap:ApiKey"];
        var apiUrl = configuration["OpenWeatherMap:ApiUrl"];
        
        var response = await httpClient.GetAsync(apiUrl + $"weather?q={city}&appid={apiKey}&units=metric");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<WeatherData>(content);
        if (weatherData is null)
        {
            return null;
        }

        weather = weatherData.ToFarmWeatherDto();
        cache.Set(city, weather, _cacheDuration);
        return weather;
    }
}