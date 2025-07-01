using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using NotificationService.Models.Dtos;
using NotificationService.Services;
using Shared.Models.Events;

namespace NotificationService.EventConsumers;

public class TaskAssignedEventConsumer(
    INotificationService notificationService,
    IHubContext<NotificationHub> hubContext,
    ILogger<ReportCreatedEventConsumer> logger
) : IConsumer<TaskAssignedEvent>
{
    public async Task Consume(ConsumeContext<TaskAssignedEvent> context)
    {
        var createdNotification = await notificationService.CreateNotificationAsync(new NotificationDto(
            Guid.NewGuid(), // Will be set by the service
            "NewTask",
            context.Message.TaskId,
            context.Message.CreatedByUserId,
            context.Message.CreatedAt,
            context.Message.FarmId,
            context.Message.AssignedUserId,
            false
        ));

        var groupName = $"Farm_{context.Message.FarmId}";

        try
        {
            await hubContext.Clients.Group(groupName)
                .SendAsync("NewTaskNotification", createdNotification); // Client listens for "NewTaskNotification"

            logger.LogInformation("Sent SignalR notification for new task {TaskId} to group {GroupName}",
                context.Message.TaskId, groupName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending SignalR notification for TaskId: {TaskId}", context.Message.TaskId);
            throw;
        }
    }
}