using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using NotificationService.Models.Dtos;
using NotificationService.Services;
using Shared.Models.Events;

namespace NotificationService.EventConsumers;

public class ReportCreatedEventConsumer(
    INotificationService notificationService,
    IHubContext<NotificationHub> hubContext,
    ILogger<ReportCreatedEventConsumer> logger
) : IConsumer<ReportCreatedEvent>
{
    public async Task Consume(ConsumeContext<ReportCreatedEvent> context)
    {
        var createdNotification = await notificationService.CreateNotificationAsync(new NotificationDto(
            Guid.NewGuid(), // Will be set by the service
            "NewReport",
            context.Message.ReportId,
            context.Message.CreatedByUserId,
            context.Message.CreatedAt,
            context.Message.FarmId,
            null,
            false
        ));

        var groupName = $"Farm_{context.Message.FarmId}";

        try
        {
            await hubContext.Clients.Group(groupName)
                .SendAsync("NewReportNotification", createdNotification); // Client listens for "NewReportNotification"

            logger.LogInformation("Sent SignalR notification for new report {ReportId} to group {GroupName}",
                context.Message.ReportId, groupName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending SignalR notification for ReportId: {ReportId}", context.Message.ReportId);
            throw;
        }
    }
}