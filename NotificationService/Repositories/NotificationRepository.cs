using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Models.Entities;

namespace NotificationService.Repositories;

public class NotificationRepository(NotificationDbContext dbContext) : INotificationRepository
{
    public async ValueTask<IEnumerable<NotificationEntity>> GetNotificationsByFarmIdAsync(Guid farmId,
        bool includeRead = false)
    {
        if (farmId == Guid.Empty)
        {
            throw new ArgumentException("Farm ID cannot be empty.", nameof(farmId));
        }

        var query = dbContext.Notifications.AsQueryable()
            .Where(n => n.TargetFarmId == farmId);

        if (!includeRead)
        {
            query = query
                .Where(n => !n.IsRead)
                .Where(n => n.Timestamp > DateTime.UtcNow.AddDays(-7)); // Only return notifications older than 7 day
        }

        return await query.ToListAsync();
    }

    public async ValueTask<IEnumerable<NotificationEntity>> GetNotificationsByTargetUserIdAsync(
        Guid userId,
        Guid farmId,
        bool includeRead = false
    )
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        }

        var query = dbContext.Notifications.AsQueryable()
            .Where(n => n.TargetFarmId == farmId)
            .Where(n => n.TargetUserId == userId);

        if (!includeRead)
        {
            query = query
                .Where(n => !n.IsRead)
                .Where(n => n.Timestamp > DateTime.UtcNow.AddDays(-7)); // Only return notifications older than 7 day
        }

        return await query.ToListAsync();
    }

    public async ValueTask<NotificationEntity?> GetNotificationByIdAsync(Guid notificationId)
    {
        if (notificationId == Guid.Empty)
        {
            throw new ArgumentException("Notification ID cannot be empty.", nameof(notificationId));
        }

        return await dbContext.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId);
    }

    public async ValueTask<NotificationEntity> CreateNotificationAsync(NotificationEntity notification)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification), "Notification cannot be null.");
        }

        if (notification.Id != Guid.Empty)
        {
            throw new ArgumentException("Notification ID should not be set for new notifications.",
                nameof(notification));
        }

        dbContext.Notifications.Add(notification);
        await dbContext.SaveChangesAsync();

        return notification;
    }

    public async ValueTask<bool> UpdateNotificationAsync(NotificationEntity notification)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification), "Notification cannot be null.");
        }

        if (notification.Id == Guid.Empty)
        {
            throw new ArgumentException("Notification ID cannot be empty.", nameof(notification));
        }

        dbContext.Notifications.Update(notification);
        var affectedRows = await dbContext.SaveChangesAsync();

        return affectedRows > 0;
    }
}