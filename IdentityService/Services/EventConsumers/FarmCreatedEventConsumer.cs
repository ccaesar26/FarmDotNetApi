using IdentityService.Services.UserService;
using MassTransit;
using Shared.Models.Events;

namespace IdentityService.Services.EventConsumers;

public class FarmCreatedEventConsumer(
    IUserService userService
) : IConsumer<FarmCreatedEvent>
{
    public async Task Consume(ConsumeContext<FarmCreatedEvent> context)
    {
        var farmCreatedEvent = context.Message;
        
        var user = await userService.GetUserAsync(farmCreatedEvent.UserId);
        
        if (user is null)
        {
            return;
        }
        
        user.FarmId = farmCreatedEvent.FarmId;
        
        await userService.UpdateUserAsync(user);
    }
}