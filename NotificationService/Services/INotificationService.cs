using NotificationService.Models.Dtos;
using NotificationService.Models.Entities;

namespace NotificationService.Services;

public interface INotificationService
{
    ValueTask<IEnumerable<NotificationDto>> GetNotificationsByFarmIdAsync(Guid farmId, bool includeRead = false);
    ValueTask<IEnumerable<NotificationDto>> GetNotificationsByTargetUserIdAsync(Guid userId, Guid farmId,
        bool includeRead = false);
    ValueTask<NotificationDto?> GetNotificationByIdAsync(Guid notificationId);
    ValueTask<NotificationDto> CreateNotificationAsync(NotificationDto notification);
    ValueTask<bool> MarkNotificationAsReadAsync(Guid notificationId);
}