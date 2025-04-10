using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Extensions;
using WeatherService.Models.Dtos;
using WeatherService.Services;
using WeatherService.Services.WeatherConditionService;

namespace WeatherService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WeatherController(
    IWeatherService weatherService,
    IWeatherConditionService weatherConditionService
) : ControllerBase
{
    [HttpGet("current/{city}")]
    public async Task<IActionResult> GetWeatherByCity(string city)
    {
        var weatherData = await weatherService.GetWeatherAsync(city);

        if (weatherData is null)
            return NotFound($"Weather data unavailable for city: {city}.");

        return await BuildWeatherResponse(weatherData);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetWeatherByCoordinates([FromQuery] double latitude, [FromQuery] double longitude)
    {
        var weatherData = await weatherService.GetWeatherAsync(latitude, longitude);

        if (weatherData is null)
            return NotFound($"Weather data unavailable for coordinates: {latitude}, {longitude}.");

        return await BuildWeatherResponse(weatherData);
    }

    [HttpGet("forecast/daily")]
    public async Task<IActionResult> GetDailyForecast([FromQuery] double latitude, [FromQuery] double longitude,
        [FromQuery] int cnt = 7)
    {
        if (cnt is < 1 or > 16)
        {
            return BadRequest("The 'cnt' parameter must be between 1 and 16.");
        }

        var forecastData = await weatherService.GetDailyForecastAsync(latitude, longitude, cnt);

        if (forecastData?.list is null || forecastData.list.Count == 0)
            return NotFound($"Daily forecast data unavailable for coordinates: {latitude}, {longitude}.");

        var dailyForecastResponses = new List<DailyForecastWithAnimationResponse>();

        foreach (var dayForecast in forecastData.list)
        {
            if (dayForecast.weather.Count == 0)
                continue; // Skip if no weather conditions for the day

            var weatherCode = dayForecast.weather.First().id; // Get the primary weather code for the day
            var weatherCondition = await weatherConditionService.GetWeatherConditionAsync(weatherCode);

            if (weatherCondition is null)
                continue; // Skip if weather condition is unavailable

            var sunriseTime = DateTimeOffset.FromUnixTimeSeconds(dayForecast.sunrise).ToLocalTime().TimeOfDay;
            var sunsetTime = DateTimeOffset.FromUnixTimeSeconds(dayForecast.sunset).ToLocalTime().TimeOfDay;
            var forecastTime = DateTimeOffset.FromUnixTimeSeconds(dayForecast.dt).ToLocalTime().TimeOfDay;

            var isDay = forecastTime >= sunriseTime && forecastTime <= sunsetTime;
            var isDarkAvailable = weatherCondition.Animation.LottieDataDark is not null &&
                                  weatherCondition.Animation.LottieDataDark != string.Empty;

            var response = dayForecast.ToDailyForecastWithAnimationResponse((isDarkAvailable && !isDay
                    ? weatherCondition.Animation.FilenameDark
                    : weatherCondition.Animation.Filename)!,
                (isDarkAvailable && !isDay
                    ? weatherCondition.Animation.LottieDataDark
                    : weatherCondition.Animation.LottieData)!);

            dailyForecastResponses.Add(response);
        }

        return Ok(dailyForecastResponses);
    }

    private async Task<IActionResult> BuildWeatherResponse(FarmWeatherDto weatherData)
    {
        var weatherCondition = await weatherConditionService.GetWeatherConditionAsync(weatherData.Code);

        if (weatherCondition is null)
            return NotFound("Weather condition unavailable.");

        var response = weatherData.ToWeatherResponse();

        var isDarkAvailable = weatherCondition.Animation.LottieDataDark is not null &&
                              weatherCondition.Animation.LottieDataDark != string.Empty;

        response = response with
        {
            LottieAnimationName = (isDarkAvailable && !weatherData.IsDay()
                ? weatherCondition.Animation.FilenameDark
                : weatherCondition.Animation.Filename)!,
            LottieAnimation = (isDarkAvailable && !weatherData.IsDay()
                ? weatherCondition.Animation.LottieDataDark
                : weatherCondition.Animation.LottieData)!
        };

        return Ok(response);
    }
}