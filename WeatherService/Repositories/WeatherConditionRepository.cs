using Microsoft.EntityFrameworkCore;
using WeatherService.Data;
using WeatherService.Models.Entities;

namespace WeatherService.Repositories;

public class WeatherConditionRepository(WeatherDbContext context) : IWeatherConditionRepository
{
    public async ValueTask<WeatherCondition?> GetByCodeAsync(int code)
    {
        return await context.WeatherConditions
            .Include(wc => wc.Animation)
            .FirstOrDefaultAsync(wc => wc.Code == code);
    }
}