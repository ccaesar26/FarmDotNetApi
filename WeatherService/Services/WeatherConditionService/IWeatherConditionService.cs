using WeatherService.Models.Entities;

namespace WeatherService.Services.WeatherConditionService;

public interface IWeatherConditionService
{
    ValueTask<WeatherCondition?> GetWeatherConditionAsync(int code);
}