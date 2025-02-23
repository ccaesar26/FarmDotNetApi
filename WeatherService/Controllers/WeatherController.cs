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
    [HttpGet]
    public async Task<IActionResult> GetWeather(WeatherRequest request)
    {
        var weatherData = request.Latitude is not null && request.Longitude is not null
            ? await weatherService.GetWeatherAsync(request.Latitude.Value, request.Longitude.Value)
            : request.City is not null
                ? await weatherService.GetWeatherAsync(request.City)
                : null;

        if (weatherData is null)
        {
            return request.City is null && request.Latitude is null && request.Longitude is null
                ? BadRequest("City, latitude, or longitude must be provided.")
                : NotFound("Weather data unavailable.");
        }

        var weatherCondition = await weatherConditionService.GetWeatherConditionAsync(weatherData.Code);
        
        if (weatherCondition is null)
        {
            return NotFound("Weather condition unavailable.");
        }

        var response = weatherData.ToWeatherResponse();
        
        var isDarkAvailable = weatherCondition.Animation.FilenameDark is not null;
        
        response = response with
        {
            LottieAnimation = (isDarkAvailable
                ? weatherCondition.Animation.LottieData
                : weatherCondition.Animation.LottieDataDark)!,
            Description = weatherCondition.Description
        };
        
        return Ok(response);
    }
}