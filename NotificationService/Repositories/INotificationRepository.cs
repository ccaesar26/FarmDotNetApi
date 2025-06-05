using NotificationService.Models.Entities;

namespace NotificationService.Repositories;

public interface INotificationRepository
{
    ValueTask<IEnumerable<NotificationEntity>> GetNotificationsByFarmIdAsync(Guid farmId, bool includeRead = false);
    ValueTask<IEnumerable<NotificationEntity>> GetNotificationsByTargetUserIdAsync(Guid userId, Guid farmId,
        bool includeRead = false);
    ValueTask<NotificationEntity?> GetNotificationByIdAsync(Guid notificationId);
    ValueTask<NotificationEntity> CreateNotificationAsync(NotificationEntity notification);
    ValueTask<bool> UpdateNotificationAsync(NotificationEntity notification);
}