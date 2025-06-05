// NotificationService.Tests/UnitTests/Repositories/NotificationRepositoryTests.cs

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Models.Entities;
using NotificationService.Repositories;

namespace NotificationService.Tests.UnitTests.Repositories;

public class NotificationRepositoryTests : IDisposable
{
    private readonly NotificationDbContext _dbContext;
    private readonly NotificationRepository _repository;
    private readonly Guid _farmId1 = Guid.NewGuid();
    private readonly Guid _userId1 = Guid.NewGuid();

    public NotificationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<NotificationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for each test run
            .Options;
        _dbContext = new NotificationDbContext(options);
        _repository = new NotificationRepository(_dbContext);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        _dbContext.Notifications.AddRange(
            new NotificationEntity
            {
                Id = Guid.NewGuid(), NotificationType = "TypeA", TargetFarmId = _farmId1,
                SourceEntityId = Guid.NewGuid(), TriggeringUserId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow.AddDays(-1), IsRead = false, TargetUserId = _userId1
            },
            new NotificationEntity
            {
                Id = Guid.NewGuid(), NotificationType = "TypeB", TargetFarmId = _farmId1,
                SourceEntityId = Guid.NewGuid(), TriggeringUserId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow.AddDays(-2), IsRead = true, TargetUserId = _userId1
            },
            new NotificationEntity
            {
                Id = Guid.NewGuid(), NotificationType = "TypeC", TargetFarmId = _farmId1,
                SourceEntityId = Guid.NewGuid(), TriggeringUserId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow.AddDays(-10), IsRead = false
            }, // Old unread
            new NotificationEntity
            {
                Id = Guid.NewGuid(), NotificationType = "TypeD", TargetFarmId = Guid.NewGuid(),
                SourceEntityId = Guid.NewGuid(), TriggeringUserId = Guid.NewGuid(), Timestamp = DateTime.UtcNow,
                IsRead = false
            } // Different farm
        );
        _dbContext.SaveChanges();
    }

    [Fact]
    public async Task GetNotificationsByFarmIdAsync_IncludeReadFalse_ReturnsUnreadRecentNotificationsForFarm()
    {
        // Act
        var result = await _repository.GetNotificationsByFarmIdAsync(_farmId1, includeRead: false);

        // Assert
        var resultList = result.ToList();
        resultList.Should().HaveCount(1);
        resultList.First().NotificationType.Should().Be("TypeA");
        resultList.All(n => !n.IsRead && n.Timestamp > DateTime.UtcNow.AddDays(-7)).Should().BeTrue();
    }

    [Fact]
    public async Task GetNotificationsByFarmIdAsync_IncludeReadTrue_ReturnsAllNotificationsForFarm()
    {
        // Act
        var result = await _repository.GetNotificationsByFarmIdAsync(_farmId1, includeRead: true);

        // Assert
        var resultList = result.ToList();
        resultList.Should().HaveCount(3); // TypeA, TypeB, TypeC
        resultList.Count(n => n.TargetFarmId == _farmId1).Should().Be(3);
    }

    [Fact]
    public async Task
        GetNotificationsByTargetUserIdAsync_IncludeReadFalse_ReturnsUnreadRecentNotificationsForUserAndFarm()
    {
        // Act
        var result = await _repository.GetNotificationsByTargetUserIdAsync(_userId1, _farmId1, includeRead: false);

        // Assert
        var resultList = result.ToList();
        resultList.Should().HaveCount(1);
        resultList.First().NotificationType.Should().Be("TypeA");
    }

    [Fact]
    public async Task CreateNotificationAsync_WithValidEntity_AddsToDatabaseAndReturnsEntityWithId()
    {
        // Arrange
        var newNotification = new NotificationEntity
        {
            // ID is not set, will be generated
            NotificationType = "NewType",
            SourceEntityId = Guid.NewGuid(),
            TriggeringUserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow,
            TargetFarmId = _farmId1,
            IsRead = false
        };

        // Act
        var createdEntity = await _repository.CreateNotificationAsync(newNotification);

        // Assert
        createdEntity.Id.Should().NotBe(Guid.Empty);
        createdEntity.NotificationType.Should().Be("NewType");

        var fromDb = await _dbContext.Notifications.FindAsync(createdEntity.Id);
        fromDb.Should().NotBeNull();
        fromDb.Should().BeEquivalentTo(createdEntity);
    }

    [Fact]
    public async Task CreateNotificationAsync_WithExistingId_ThrowsArgumentException()
    {
        // Arrange
        var newNotification = new NotificationEntity
        {
            Id = Guid.NewGuid(), // Pre-set ID
            NotificationType = "NewType",
            SourceEntityId = Guid.NewGuid(),
            TriggeringUserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow,
            TargetFarmId = _farmId1,
            IsRead = false
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _repository.CreateNotificationAsync(newNotification).AsTask());
    }


    [Fact]
    public async Task UpdateNotificationAsync_ExistingNotification_UpdatesAndReturnsTrue()
    {
        // Arrange
        var notificationToUpdate = await _dbContext.Notifications.FirstAsync(n => n.NotificationType == "TypeA");
        notificationToUpdate.IsRead = true;
        notificationToUpdate.NotificationType = "TypeA_Updated";

        // Act
        var result = await _repository.UpdateNotificationAsync(notificationToUpdate);

        // Assert
        result.Should().BeTrue();
        var updatedFromDb = await _dbContext.Notifications.FindAsync(notificationToUpdate.Id);
        updatedFromDb.Should().NotBeNull();
        updatedFromDb.IsRead.Should().BeTrue();
        updatedFromDb.NotificationType.Should().Be("TypeA_Updated");
    }

    [Fact]
    public async Task GetNotificationByIdAsync_ExistingId_ReturnsNotification()
    {
        // Arrange
        var existingNotification = _dbContext.Notifications.First();

        // Act
        var result = await _repository.GetNotificationByIdAsync(existingNotification.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(existingNotification);
    }

    [Fact]
    public async Task GetNotificationByIdAsync_NonExistingId_ReturnsNull()
    {
        // Act
        var result = await _repository.GetNotificationByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }


    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted(); // Clean up the in-memory database
        _dbContext.Dispose();
    }
}