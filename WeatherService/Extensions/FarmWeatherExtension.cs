using WeatherService.Models.Dtos;

namespace WeatherService.Extensions;

public static class FarmWeatherExtension
{
    public static FarmWeatherDto ToFarmWeatherDto(this WeatherData w) => new(
        w.name,
        w.main.temp,
        w.main.feels_like,
        w.main.humidity,
        w.main.pressure,
        w.wind.speed,
        w.wind.gust,
        w.clouds.all,
        TimeOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(w.sys.sunrise).DateTime),
        TimeOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(w.sys.sunset).DateTime),
        w.weather[0].id
    );
}