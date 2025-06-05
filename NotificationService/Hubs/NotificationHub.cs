using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Hubs;

public class NotificationHub : Hub
{
    public override Task OnConnectedAsync()
    {
        var farmId = Context.User?.FindFirst("farmId")?.Value;
        
        // throw new Exception($"FarmId not found in user claims. {Context.User?.Claims.ToArray().Length}");
        if (string.IsNullOrEmpty(farmId))
        {
            throw new Exception("FarmId not found in user claims.");
            // return base.OnConnectedAsync();
        }
        
        Groups.AddToGroupAsync(Context.ConnectionId, $"Farm_{farmId}");
        Console.WriteLine($"Client {Context.ConnectionId} connected to NotificationHub for Farm Group {farmId}");
        
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var farmId = Context.User?.FindFirst("FarmId")?.Value;
        
        if (string.IsNullOrEmpty(farmId))
        {
            return base.OnDisconnectedAsync(exception);
        }
        
        Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Farm_{farmId}");
        Console.WriteLine($"Client {Context.ConnectionId} disconnected from NotificationHub for Farm Group {farmId}");
        
        return base.OnDisconnectedAsync(exception);
    }
    
    public Guid GetFarmId()
    {
        var farmId = Context.User?.FindFirst("FarmId")?.Value;
        if (string.IsNullOrEmpty(farmId))
        {
            throw new Exception("FarmId not found in user claims.");
        }
        return Guid.Parse(farmId);
    }
}