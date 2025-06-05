using FluentAssertions;
using NotificationService.MappingExtensions;
using NotificationService.Models.Dtos;
using NotificationService.Models.Entities;

namespace NotificationService.Tests.UnitTests.MappingExtensions;

public class NotificationMappingExtensionsTests
{
    [Fact]
    public void ToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new NotificationDto(
            Guid.NewGuid(), // ID in DTO is often present, but ToEntity might ignore it or set its own
            "TestType",
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            Guid.NewGuid(),
            Guid.NewGuid(),
            false
        );

        // Act
        var entity = dto.ToEntity();

        // Assert
        // entity.Id.Should().Be(Guid.Empty); // Assuming ToEntity doesn't set ID or sets to new Guid
        entity.NotificationType.Should().Be(dto.NotificationType);
        entity.SourceEntityId.Should().Be(dto.SourceEntityId);
        entity.TriggeringUserId.Should().Be(dto.TriggeringUserId);
        entity.Timestamp.Should().Be(dto.Timestamp);
        entity.TargetFarmId.Should().Be(dto.TargetFarmId);
        entity.TargetUserId.Should().Be(dto.TargetUserId);
        entity.IsRead.Should().Be(dto.IsRead);
    }

    [Fact]
    public void ToDto_ShouldMapCorrectly()
    {
        // Arrange
        var entity = new NotificationEntity
        {
            Id = Guid.NewGuid(),
            NotificationType = "EntityType",
            SourceEntityId = Guid.NewGuid(),
            TriggeringUserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow,
            TargetFarmId = Guid.NewGuid(),
            TargetUserId = Guid.NewGuid(),
            IsRead = true
        };

        // Act
        var dto = entity.ToDto();

        // Assert
        dto.Id.Should().Be(entity.Id);
        dto.NotificationType.Should().Be(entity.NotificationType);
        dto.SourceEntityId.Should().Be(entity.SourceEntityId);
        dto.TriggeringUserId.Should().Be(entity.TriggeringUserId);
        dto.Timestamp.Should().Be(entity.Timestamp);
        dto.TargetFarmId.Should().Be(entity.TargetFarmId);
        dto.TargetUserId.Should().Be(entity.TargetUserId);
        dto.IsRead.Should().Be(entity.IsRead);
    }
}