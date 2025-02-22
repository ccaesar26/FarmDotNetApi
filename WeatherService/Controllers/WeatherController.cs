using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Models.Dtos;
using WeatherService.Services;

namespace WeatherService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WeatherController(
    IWeatherService weatherService
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

        return weatherData is not null ? Ok(weatherData) 
            : request.City is null && request.Latitude is null && request.Longitude is null
                ? BadRequest("City, latitude, or longitude must be provided.") 
                : NotFound("Weather data unavailable.");
    }

}