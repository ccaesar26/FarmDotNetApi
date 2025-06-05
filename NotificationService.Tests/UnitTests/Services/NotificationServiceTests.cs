// NotificationService.Tests/UnitTests/Services/NotificationServiceTests.cs

using FluentAssertions;
using Moq;
using NotificationService.MappingExtensions;
using NotificationService.Models.Dtos;
using NotificationService.Models.Entities;
using NotificationService.Repositories;

namespace NotificationService.Tests.UnitTests.Services;

public class NotificationServiceTests
{
    private readonly Mock<INotificationRepository> _mockRepository;
    private readonly NotificationService.Services.NotificationService _service; // Fully qualify to avoid conflict

    public NotificationServiceTests()
    {
        _mockRepository = new Mock<INotificationRepository>();
        _service = new NotificationService.Services.NotificationService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetNotificationsByFarmIdAsync_CallsRepositoryAndMapsToDto()
    {
        // Arrange
        var farmId = Guid.NewGuid();
        var entities = new List<NotificationEntity>
        {
            new NotificationEntity { Id = Guid.NewGuid(), TargetFarmId = farmId, NotificationType="T1", SourceEntityId=Guid.NewGuid(), TriggeringUserId=Guid.NewGuid(), Timestamp=DateTime.UtcNow, IsRead=false }
        };
        _mockRepository.Setup(r => r.GetNotificationsByFarmIdAsync(farmId, false)).ReturnsAsync(entities);

        // Act
        var result = await _service.GetNotificationsByFarmIdAsync(farmId);

        // Assert
        _mockRepository.Verify(r => r.GetNotificationsByFarmIdAsync(farmId, false), Times.Once);
        var resultList = result.ToList();
        resultList.Should().HaveCount(1);
        resultList.First().Should().BeEquivalentTo(entities.First().ToDto());
    }
    
    [Fact]
    public async Task CreateNotificationAsync_MapsToEntity_CallsRepository_MapsBackToDto()
    {
        // Arrange
        var dto = new NotificationDto(Guid.Empty, "NewType", Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, false);
        // var entityToCreate = dto.ToEntity(); // ID should be Guid.Empty or handled by ToEntity
        var createdEntity = new NotificationEntity
        {
            Id = Guid.NewGuid(), /* other props from dto */ 
            NotificationType = dto.NotificationType,
            SourceEntityId = dto.SourceEntityId,
            TriggeringUserId = dto.TriggeringUserId,
            Timestamp = dto.Timestamp,
            TargetFarmId = dto.TargetFarmId,
            TargetUserId = dto.TargetUserId,
            IsRead = dto.IsRead
        }; // Simulate repo setting the ID

        _mockRepository.Setup(r => r.CreateNotificationAsync(It.Is<NotificationEntity>(e => e.NotificationType == dto.NotificationType)))
                       .ReturnsAsync(createdEntity);
        
        // Act
        var result = await _service.CreateNotificationAsync(dto);

        // Assert
        _mockRepository.Verify(r => r.CreateNotificationAsync(It.Is<NotificationEntity>(
            e => e.NotificationType == dto.NotificationType &&
                 e.SourceEntityId == dto.SourceEntityId // Add more property checks if needed
        )), Times.Once);
        
        result.Should().NotBeNull();
        result.Id.Should().Be(createdEntity.Id);
        result.Should().BeEquivalentTo(createdEntity.ToDto());
    }

    [Fact]
    public async Task MarkNotificationAsReadAsync_NotificationExists_UpdatesAndReturnsTrue()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        var entity = new NotificationEntity { Id = notificationId, IsRead = false, NotificationType="T", SourceEntityId=Guid.NewGuid(), TriggeringUserId=Guid.NewGuid(), Timestamp=DateTime.UtcNow, TargetFarmId=Guid.NewGuid() };
        _mockRepository.Setup(r => r.GetNotificationByIdAsync(notificationId)).ReturnsAsync(entity);
        _mockRepository.Setup(r => r.UpdateNotificationAsync(It.Is<NotificationEntity>(e => e.Id == notificationId && e.IsRead))).ReturnsAsync(true);

        // Act
        var result = await _service.MarkNotificationAsReadAsync(notificationId);

        // Assert
        result.Should().BeTrue();
        _mockRepository.Verify(r => r.GetNotificationByIdAsync(notificationId), Times.Once);
        _mockRepository.Verify(r => r.UpdateNotificationAsync(It.Is<NotificationEntity>(e => e.Id == notificationId && e.IsRead)), Times.Once);
    }

    [Fact]
    public async Task MarkNotificationAsReadAsync_NotificationNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        _mockRepository.Setup(r => r.GetNotificationByIdAsync(notificationId)).ReturnsAsync((NotificationEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.MarkNotificationAsReadAsync(notificationId).AsTask());
    }
    
    [Fact]
    public async Task GetNotificationByIdAsync_WhenNotificationExists_ReturnsDto()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        var entity = new NotificationEntity { Id = notificationId, /* fill other required properties */ NotificationType="Test", SourceEntityId=Guid.NewGuid(), TriggeringUserId=Guid.NewGuid(), Timestamp=DateTime.UtcNow, TargetFarmId=Guid.NewGuid(), IsRead=false };
        _mockRepository.Setup(r => r.GetNotificationByIdAsync(notificationId)).ReturnsAsync(entity);

        // Act
        var result = await _service.GetNotificationByIdAsync(notificationId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(entity.ToDto());
    }

    [Fact]
    public async Task GetNotificationByIdAsync_WhenNotificationDoesNotExist_ReturnsNull()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        _mockRepository.Setup(r => r.GetNotificationByIdAsync(notificationId)).ReturnsAsync((NotificationEntity?)null);

        // Act
        var result = await _service.GetNotificationByIdAsync(notificationId);

        // Assert
        result.Should().BeNull();
    }
}