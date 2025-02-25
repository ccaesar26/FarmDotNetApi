using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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