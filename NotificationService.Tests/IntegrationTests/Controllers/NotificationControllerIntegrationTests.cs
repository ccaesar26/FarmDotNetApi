// NotificationService.Tests/IntegrationTests/Controllers/NotificationControllerIntegrationTests.cs

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NotificationService.Data;
using NotificationService.Models.Dtos;
using NotificationService.Models.Entities;

// Assuming your Program class is in the root namespace of NotificationService project
// If not, adjust the namespace. You might need to make Program public or use InternalsVisibleTo
// For .NET 6+, TStartup is often Program.
// Using NotificationService; 

namespace NotificationService.Tests.IntegrationTests.Controllers;

// Replace 'Program' with your actual startup class if different (e.g. 'Startup')
// You might need to make Program class public, or add:
// [assembly: InternalsVisibleTo("NotificationService.Tests")] in NotificationService.csproj
public class NotificationControllerIntegrationTests : IClassFixture<NotificationServiceAppFactory<Program>>
{
    private readonly NotificationServiceAppFactory<Program> _factory;
    private readonly Guid _defaultFarmId = Guid.NewGuid();
    private readonly Guid _defaultUserId = Guid.NewGuid();

    public NotificationControllerIntegrationTests(NotificationServiceAppFactory<Program> factory)
    {
        _factory = factory;

        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<NotificationDbContext>();
        db.Database.EnsureCreated();
    }

    private async Task SeedNotification(NotificationEntity notification)
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        if (!await context.Notifications.AnyAsync(n => n.Id == notification.Id))
        {
            context.Notifications.Add(notification);
            await context.SaveChangesAsync();
        }
    }

    private async Task ClearNotifications()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        context.Notifications.RemoveRange(context.Notifications);
        await context.SaveChangesAsync();
    }


    [Fact]
    public async Task GetAllNotificationsAsync_AsManager_ReturnsNotificationsForFarm()
    {
        // Arrange
        await ClearNotifications(); // Ensure a clean state

        var authOptionsForManager = new TestAuthHandlerOptions
        {
            Role = "Manager",
            FarmId = _defaultFarmId, // Or a specific FarmId for this test
            UserId = _defaultUserId // Or a specific UserId for this test
        };

        // Recreate the client if the role needs to be applied from factory settings for TestAuthHandler
        var clientForManager = _factory.CreateClientWithAuth(authOptionsForManager);
        clientForManager.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);


        var notif1 = new NotificationEntity
        {
            Id = Guid.NewGuid(), 
            NotificationType = "N1", 
            TargetFarmId = _defaultFarmId,
            SourceEntityId = Guid.NewGuid(), 
            TriggeringUserId = Guid.NewGuid(), 
            Timestamp = DateTime.UtcNow,
            IsRead = false
        };
        var notif2 = new NotificationEntity
        {
            Id = Guid.NewGuid(), 
            NotificationType = "N2", 
            TargetFarmId = Guid.NewGuid(),
            SourceEntityId = Guid.NewGuid(), 
            TriggeringUserId = Guid.NewGuid(), 
            Timestamp = DateTime.UtcNow,
            IsRead = false
        }; // Different farm
        await SeedNotification(notif1);
        await SeedNotification(notif2);

        // Act
        var response = await clientForManager.GetAsync("/api/Notification/all");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var notifications = await response.Content.ReadFromJsonAsync<NotificationDto[]>();
        notifications.Should().HaveCount(1);
        notifications.Should().ContainSingle(n => n.Id == notif1.Id);
    }
    
    [Fact]
    public async Task GetUserNotificationsAsync_ReturnsNotificationsForUserAndFarm()
    {
        // Arrange
        await ClearNotifications();
        
        var authOptionsForWorker = new TestAuthHandlerOptions // Assuming "ManagerAndWorkers" policy accepts "Worker" role
        {
            Role = "Worker", // Or "Manager" if that's what your policy implies
            FarmId = _defaultFarmId,
            UserId = _defaultUserId
        };
        
        var clientForUser = _factory.CreateClientWithAuth(authOptionsForWorker);
        clientForUser.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
    
        var userNotif = new NotificationEntity
        {
            Id = Guid.NewGuid(), 
            NotificationType = "UserN1", 
            TargetFarmId = _defaultFarmId,
            TargetUserId = _defaultUserId, 
            SourceEntityId = Guid.NewGuid(), 
            TriggeringUserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow, 
            IsRead = false
        };
        var otherUserNotif = new NotificationEntity
        {
            Id = Guid.NewGuid(), 
            NotificationType = "UserN2", 
            TargetFarmId = _defaultFarmId,
            TargetUserId = Guid.NewGuid(), 
            SourceEntityId = Guid.NewGuid(), 
            TriggeringUserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow, 
            IsRead = false
        };
        await SeedNotification(userNotif);
        await SeedNotification(otherUserNotif);
    
        // Act
        var response = await clientForUser.GetAsync("/api/Notification/byUser");
    
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var notifications = await response.Content.ReadFromJsonAsync<NotificationDto[]>();
        notifications.Should().HaveCount(1);
        notifications.Should().ContainSingle(n => n.Id == userNotif.Id && n.TargetUserId == _defaultUserId);
    }
    
    [Fact]
    public async Task MarkNotificationAsReadAsync_ValidId_MarksAsReadAndReturnsNoContent()
    {
        // Arrange
        await ClearNotifications();
        
        var authOptionsForUser = new TestAuthHandlerOptions
        {
            Role = "Worker", // Assuming this role allows marking notifications as read
            FarmId = _defaultFarmId,
            UserId = _defaultUserId
        };
        
        var clientForUser = _factory.CreateClientWithAuth(authOptionsForUser);
        clientForUser.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
    
        var notificationId = Guid.NewGuid();
        var unreadNotif = new NotificationEntity
        {
            Id = notificationId, 
            NotificationType = "Unread", 
            TargetFarmId = _defaultFarmId,
            TargetUserId = _defaultUserId, 
            SourceEntityId = Guid.NewGuid(), 
            TriggeringUserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow, 
            IsRead = false
        };
        await SeedNotification(unreadNotif);
    
        // Act
        var response = await clientForUser.PostAsJsonAsync("/api/Notification/markAsRead", notificationId);
    
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    
        // Verify in DB
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        var dbNotification = await context.Notifications.FindAsync(notificationId);
        dbNotification.Should().NotBeNull();
        dbNotification.IsRead.Should().BeTrue();
    }
    
    [Fact]
    public async Task MarkNotificationAsReadAsync_NotFound_ReturnsNotFound()
    {
        // Arrange
        await ClearNotifications();
        
        var authOptionsForUser = new TestAuthHandlerOptions
        {
            Role = "Manager", // Assuming this role allows marking notifications as read
            FarmId = _defaultFarmId,
            UserId = _defaultUserId
        };
        var clientForUser = _factory.CreateClientWithAuth(authOptionsForUser);
        clientForUser.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
    
        var nonExistentId = Guid.NewGuid();
    
        // Act
        var response = await clientForUser.PostAsJsonAsync("/api/Notification/markAsRead", nonExistentId);
    
        response.StatusCode.Should().Be(HttpStatusCode.NotFound); 
    }
}