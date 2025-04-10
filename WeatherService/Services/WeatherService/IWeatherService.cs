using WeatherService.Models.Dtos;

namespace WeatherService.Services;

public interface IWeatherService
{
    ValueTask<FarmWeatherDto?> GetWeatherAsync(string city);
    ValueTask<FarmWeatherDto?> GetWeatherAsync(double latitude, double longitude);
    ValueTask<DailyForecastResponseDto?> GetDailyForecastAsync(double latitude, double longitude, int cnt = 16);
}