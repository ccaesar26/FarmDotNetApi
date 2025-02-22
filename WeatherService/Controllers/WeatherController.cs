using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Services;

namespace WeatherService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WeatherController(
    IWeatherService weatherService
) : ControllerBase
{
    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        var weatherData = await weatherService.GetWeatherAsync(city);
        return weatherData != null ? Ok(weatherData) : NotFound("Weather data unavailable.");
    }
}