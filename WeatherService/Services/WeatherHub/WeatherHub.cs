using Microsoft.AspNetCore.SignalR;

namespace WeatherService.Services.WeatherHub;

public class WeatherHub : Hub
{
    public async Task NotifyWeatherUpdate()
    {
        await Clients.All.SendAsync("WeatherUpdated");
    }
}