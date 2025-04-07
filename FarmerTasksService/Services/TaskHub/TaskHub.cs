using Microsoft.AspNetCore.SignalR;

namespace FarmerTasksService.Services.TaskHub;

public class TaskHub : Hub
{
    public async Task NotifyTaskCreated()
    {
        await Clients.All.SendAsync("TaskCreated");
    }
    
    public async Task NotifyTaskUpdated()
    {
        await Clients.All.SendAsync("TaskUpdated");
    }
}