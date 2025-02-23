using WeatherService.Models.Entities;
using WeatherService.Repositories;

namespace WeatherService.Services.WeatherConditionService;

public class WeatherConditionService(IWeatherConditionRepository weatherConditionRepository) : IWeatherConditionService
{
    public async ValueTask<WeatherCondition?> GetWeatherConditionAsync(int code)
    {
        return await weatherConditionRepository.GetByCodeAsync(code);
    }
}