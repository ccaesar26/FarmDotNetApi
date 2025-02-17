using IdentityService.Services.UserService;
using MassTransit;
using Shared.Models.Events;

namespace IdentityService.Services.EventConsumers;

public class UserProfileCreatedEventConsumer(
    IUserService userService
) : IConsumer<UserProfileCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserProfileCreatedEvent> context)
    {
        var userId = context.Message.UserId;
        var userProfileId = context.Message.UserProfileId;

        var user = await userService.GetUserAsync(userId);
        if (user is not null)
        {
            user.UserProfileId = userProfileId;
            await userService.UpdateUserAsync(user);
        }
    }
}