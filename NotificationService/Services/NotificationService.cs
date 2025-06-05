using NotificationService.MappingExtensions;
using NotificationService.Models.Dtos;
using NotificationService.Repositories;

namespace NotificationService.Services;

public class NotificationService(INotificationRepository notificationRepository) : INotificationService
{
    public async ValueTask<IEnumerable<NotificationDto>> GetNotificationsByFarmIdAsync(Guid farmId,
        bool includeRead = false)
    {
        if (farmId == Guid.Empty)
        {
            throw new ArgumentException("Farm ID cannot be empty.", nameof(farmId));
        }

        var notifications = await notificationRepository.GetNotificationsByFarmIdAsync(farmId, includeRead);
        return notifications.Select(n => n.ToDto());
    }

    public async ValueTask<IEnumerable<NotificationDto>> GetNotificationsByTargetUserIdAsync(
        Guid userId, Guid farmId, bool includeRead = false
    )
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        }

        var notifications =
            await notificationRepository.GetNotificationsByTargetUserIdAsync(userId, farmId, includeRead);
        return notifications.Select(n => n.ToDto());
    }

    public async ValueTask<NotificationDto?> GetNotificationByIdAsync(Guid notificationId)
    {
        if (notificationId == Guid.Empty)
        {
            throw new ArgumentException("Notification ID cannot be empty.", nameof(notificationId));
        }

        var notificationEntity = await notificationRepository.GetNotificationByIdAsync(notificationId);
        return notificationEntity?.ToDto();
    }

    public async ValueTask<NotificationDto> CreateNotificationAsync(NotificationDto notification)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification), "Notification cannot be null.");
        }

        var notificationEntity = notification.ToEntity();
        var createdNotification = await notificationRepository.CreateNotificationAsync(notificationEntity);
        return createdNotification.ToDto();
    }

    public async ValueTask<bool> MarkNotificationAsReadAsync(Guid notificationId)
    {
        if (notificationId == Guid.Empty)
        {
            throw new ArgumentException("Notification ID cannot be empty.", nameof(notificationId));
        }

        var notificationEntity = await notificationRepository.GetNotificationByIdAsync(notificationId);
        if (notificationEntity == null)
        {
            throw new KeyNotFoundException($"Notification with ID {notificationId} not found.");
        }

        notificationEntity.IsRead = true;
        return await notificationRepository.UpdateNotificationAsync(notificationEntity);
    }
}