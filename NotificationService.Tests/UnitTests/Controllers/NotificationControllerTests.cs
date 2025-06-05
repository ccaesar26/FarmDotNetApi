// NotificationService.Tests/UnitTests/Controllers/NotificationControllerTests.cs

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NotificationService.Controllers;
using NotificationService.Models.Dtos;
using NotificationService.Services; // Your INotificationService
using Shared.FarmAuthorizationService; // Your IFarmAuthorizationService

namespace NotificationService.Tests.UnitTests.Controllers;

public class NotificationControllerTests
{
    private readonly Mock<INotificationService> _mockNotificationService;
    private readonly Mock<IFarmAuthorizationService> _mockAuthService;
    private readonly NotificationController _controller;

    private readonly Guid _testFarmId = Guid.NewGuid();
    // private readonly Guid _testUserId = Guid.NewGuid();

    public NotificationControllerTests()
    {
        _mockNotificationService = new Mock<INotificationService>();
        _mockAuthService = new Mock<IFarmAuthorizationService>();
        var mockLogger = new Mock<ILogger<NotificationController>>();

        _controller = new NotificationController(
            _mockNotificationService.Object,
            _mockAuthService.Object,
            mockLogger.Object
        );
    }

    [Fact]
    public async Task GetAllNotificationsAsync_FarmContextMissing_ReturnsUnauthorized()
    {
        // Arrange
        _mockAuthService.Setup(a => a.GetFarmId()).Returns((Guid?)null);

        // Act
        var result = await _controller.GetAllNotificationsAsync();

        // Assert
        result.Should().BeOfType<UnauthorizedObjectResult>()
            .Which.Value.Should().Be("Farm context missing.");
    }

    [Fact]
    public async Task GetAllNotificationsAsync_ValidFarmId_ReturnsOkWithNotifications()
    {
        // Arrange
        _mockAuthService.Setup(a => a.GetFarmId()).Returns(_testFarmId);
        var notifications = new List<NotificationDto> { new NotificationDto(Guid.NewGuid(), "T1", Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, _testFarmId, null, false) };
        _mockNotificationService.Setup(s => s.GetNotificationsByFarmIdAsync(_testFarmId, true))
            .ReturnsAsync(notifications);

        // Act
        var result = await _controller.GetAllNotificationsAsync();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedNotifications = okResult.Value.Should().BeAssignableTo<NotificationDto[]>().Subject;
        returnedNotifications.Should().BeEquivalentTo(notifications.ToArray());
    }

    [Fact]
    public async Task GetUserNotificationsAsync_UserOrFarmContextMissing_ReturnsUnauthorized()
    {
        // Arrange
        _mockAuthService.Setup(a => a.GetUserId()).Returns((Guid?)null);
        _mockAuthService.Setup(a => a.GetFarmId()).Returns(_testFarmId);

        // Act
        var result = await _controller.GetUserNotificationsAsync();

        // Assert
        result.Should().BeOfType<UnauthorizedObjectResult>()
            .Which.Value.Should().Be("User or Farm context missing.");
    }
    
    [Fact]
    public async Task MarkNotificationAsReadAsync_ValidId_ReturnsNoContent()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        _mockNotificationService.Setup(s => s.MarkNotificationAsReadAsync(notificationId)).ReturnsAsync(true);

        // Act
        var result = await _controller.MarkNotificationAsReadAsync(notificationId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task MarkNotificationAsReadAsync_NotificationNotFound_ReturnsNotFound()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        _mockNotificationService.Setup(s => s.MarkNotificationAsReadAsync(notificationId)).ReturnsAsync(false);

        // Act
        var result = await _controller.MarkNotificationAsReadAsync(notificationId);

        // Assert
        var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFoundResult.Value.Should().Be("Notification not found or already marked as read.");
    }
    
    [Fact]
    public async Task MarkNotificationAsReadAsync_EmptyGuid_ReturnsBadRequest()
    {
        // Arrange
        var notificationId = Guid.Empty;

        // Act
        var result = await _controller.MarkNotificationAsReadAsync(notificationId);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().Be("Notification ID cannot be empty.");
    }
}