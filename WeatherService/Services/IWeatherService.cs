using WeatherService.Models.Dtos;

namespace WeatherService.Services;

public interface IWeatherService
{
    ValueTask<FarmWeatherDto?> GetWeatherAsync(string city);
}