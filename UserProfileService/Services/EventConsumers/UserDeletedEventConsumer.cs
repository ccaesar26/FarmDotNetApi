using MassTransit;
using Shared.Models.Events;
using UserProfileService.Services.UserProfileService;

namespace UserProfileService.Services.EventConsumers;

public class UserDeletedEventConsumer(
    IUserProfileService userProfileService
) : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var userDeletedEvent = context.Message;
        
        var userProfile = await userProfileService.GetUserProfileByUserIdAsync(userDeletedEvent.UserId);
        
        await userProfileService.DeleteUserProfileAsync(userProfile?.Id ?? Guid.Empty);
    }
}