using WeatherService.Models.Entities;

namespace WeatherService.Repositories;

public interface IWeatherConditionRepository
{
    ValueTask<WeatherCondition?> GetByCodeAsync(int code); 
}