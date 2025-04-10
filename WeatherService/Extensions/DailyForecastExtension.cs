using WeatherService.Models.Dtos;

namespace WeatherService.Extensions;

public static class DailyForecastExtension
{
    public static DailyForecastWithAnimationResponse ToDailyForecastWithAnimationResponse(
        this DailyForecastItemDto dayForecast,
        string lottieAnimationName,
        string lottieAnimation
    ) => new DailyForecastWithAnimationResponse(
        DateTimeOffset.FromUnixTimeSeconds(dayForecast.dt).ToLocalTime().DateTime.ToShortDateString(),
        dayForecast.temp.day,
        dayForecast.temp.min,
        dayForecast.temp.max,
        dayForecast.humidity,
        dayForecast.pressure,
        dayForecast.speed,
        dayForecast.gust,
        dayForecast.clouds,
        dayForecast.pop,
        dayForecast.rain,
        dayForecast.snow,
        lottieAnimationName,
        lottieAnimation
    );
}