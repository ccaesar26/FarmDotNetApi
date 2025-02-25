using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace WeatherService.Services.WeatherJobs;

public class WeatherUpdateJob(
    IHubContext<WeatherHub.WeatherHub> hubContext,
    ILogger<WeatherUpdateJob> logger
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Triggering weather update notification...");
        
        try
        {
            // Notify all connected clients to fetch new weather data
            await hubContext.Clients.All.SendAsync("WeatherUpdated");
            logger.LogInformation("Weather update notification sent.");
        }
        catch (Exception ex)
        {
            logger.LogError($"Error sending SignalR notification: {ex.Message}");
        }
    }
}