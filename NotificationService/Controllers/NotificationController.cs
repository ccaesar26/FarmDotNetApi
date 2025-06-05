using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Services;
using Shared.FarmAuthorizationService;

namespace NotificationService.Controllers;

[ApiController]
[Authorize]
[Route("/api/[controller]")]
public class NotificationController(
    INotificationService notificationService,
    IFarmAuthorizationService authorizationService,
    ILogger<NotificationController> logger
) : ControllerBase
{
    [HttpGet("all")]
    [Authorize(Policy = "ManagerOnly")] // Only managers can view all notifications
    public async Task<IActionResult> GetAllNotificationsAsync()
    {
        var farmId = authorizationService.GetFarmId();
        if (!farmId.HasValue)
        {
            return Unauthorized("Farm context missing.");
        }

        try
        {
            var notifications =
                await notificationService.GetNotificationsByFarmIdAsync(farmId.Value, includeRead: true);

            return Ok(notifications.ToArray());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving notifications for FarmId: {FarmId}", farmId);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("byUser")]
    [Authorize(Policy = "ManagerAndWorkers")] // Both managers and workers can view their own notifications
    public async Task<IActionResult> GetUserNotificationsAsync()
    {
        var userId = authorizationService.GetUserId();
        var farmId = authorizationService.GetFarmId();

        if (!userId.HasValue || !farmId.HasValue)
        {
            return Unauthorized("User or Farm context missing.");
        }

        try
        {
            var notifications = await notificationService
                .GetNotificationsByTargetUserIdAsync(userId.Value, farmId.Value, includeRead: true);

            return Ok(notifications.ToArray());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving notifications for UserId: {UserId} in FarmId: {FarmId}", userId,
                farmId);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost("markAsRead")]
    [Authorize(Policy = "ManagerAndWorkers")] // Both managers and workers can mark notifications as read
    public async Task<IActionResult> MarkNotificationAsReadAsync([FromBody] Guid notificationId)
    {
        if (notificationId == Guid.Empty)
        {
            return BadRequest("Notification ID cannot be empty.");
        }

        try
        {
            var success = await notificationService.MarkNotificationAsReadAsync(notificationId);
            if (!success)
            {
                return NotFound("Notification not found or already marked as read.");
            }

            return NoContent(); // 204 No Content
        }
        catch (KeyNotFoundException knfEx)
        {
            logger.LogWarning(knfEx, "Notification not found for NotificationId: {NotificationId}", notificationId);
            return NotFound("Notification not found.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error marking notification as read for NotificationId: {NotificationId}", notificationId);
            return StatusCode(500, "Internal server error");
        }
    }
}