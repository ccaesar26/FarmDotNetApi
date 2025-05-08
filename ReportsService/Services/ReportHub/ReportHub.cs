using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ReportsService.Models.Events;

namespace ReportsService.Services.ReportHub;

public class ReportHub : Hub
{
    public async Task NotifyReportCreated(ReportCreatedEvent eventData)
    {
        await Clients.All.SendAsync("ReportCreated", eventData);
    }

    public async Task NotifyReportUpdated(string message)
    {
        await Clients.All.SendAsync("ReportUpdated", message);
    }

    public async Task NotifyReportDeleted(string message)
    {
        await Clients.All.SendAsync("ReportDeleted", message);
    }
}