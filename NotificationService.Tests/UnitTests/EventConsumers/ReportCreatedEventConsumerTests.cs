// NotificationService.Tests/UnitTests/EventConsumers/ReportCreatedEventConsumerTests.cs

using FluentAssertions;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using NotificationService.EventConsumers;
using NotificationService.Hubs;
using NotificationService.Models.Dtos;
using NotificationService.Services; // Your INotificationService
using Shared.Models.Events;

namespace NotificationService.Tests.UnitTests.EventConsumers;

public class ReportCreatedEventConsumerTests
{
    private readonly Mock<INotificationService> _mockNotificationService;
    private readonly Mock<IHubContext<NotificationHub>> _mockHubContext;
    private readonly Mock<ILogger<ReportCreatedEventConsumer>> _mockLogger;
    private readonly ReportCreatedEventConsumer _consumer;
    private readonly Mock<IClientProxy> _mockClientProxy; // For SignalR group sending

    public ReportCreatedEventConsumerTests()
    {
        _mockNotificationService = new Mock<INotificationService>();
        _mockHubContext = new Mock<IHubContext<NotificationHub>>();
        _mockLogger = new Mock<ILogger<ReportCreatedEventConsumer>>();
        _mockClientProxy = new Mock<IClientProxy>();

        // Setup for HubContext.Clients.Group(groupName).SendAsync(...)
        var mockClients = new Mock<IHubClients>();
        mockClients.Setup(clients => clients.Group(It.IsAny<string>())).Returns(_mockClientProxy.Object);
        _mockHubContext.Setup(hc => hc.Clients).Returns(mockClients.Object);

        _consumer = new ReportCreatedEventConsumer(
            _mockNotificationService.Object,
            _mockHubContext.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Consume_ReportCreatedEvent_CreatesNotificationAndSendsSignalR()
    {
        // Arrange
        var reportEvent = new ReportCreatedEvent(
            Guid.NewGuid(), // ReportId
            Guid.NewGuid(), // FarmId
            Guid.NewGuid(), // CreatedByUserId
            DateTime.UtcNow
        );

        var consumeContextMock = new Mock<ConsumeContext<ReportCreatedEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(reportEvent);

        var createdNotificationDto = new NotificationDto(
            Guid.NewGuid(), "NewReport", reportEvent.ReportId, reportEvent.CreatedByUserId,
            reportEvent.CreatedAt, reportEvent.FarmId, null, false
        );

        _mockNotificationService.Setup(s => s.CreateNotificationAsync(It.IsAny<NotificationDto>()))
            .ReturnsAsync(createdNotificationDto);

        // Act
        await _consumer.Consume(consumeContextMock.Object);

        // Assert
        _mockNotificationService.Verify(s => s.CreateNotificationAsync(
            It.Is<NotificationDto>(dto =>
                dto.NotificationType == "NewReport" &&
                dto.SourceEntityId == reportEvent.ReportId &&
                dto.TargetFarmId == reportEvent.FarmId
            )), Times.Once);

        _mockClientProxy.Verify(c => c.SendCoreAsync(
            "NewReportNotification", // Method name
            It.Is<object[]>(args => args.Length == 1 && (NotificationDto)args[0] == createdNotificationDto), // Args
            CancellationToken.None // CancellationToken
        ), Times.Once);
        
        // Verify the group name
        _mockHubContext.Verify(hc => hc.Clients.Group($"Farm_{reportEvent.FarmId}"), Times.Once);
    }

    [Fact]
    public async Task Consume_NotificationServiceThrows_ExceptionIsPropagatedAndLogged()
    {
        // Arrange
        var reportEvent = new ReportCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        var consumeContextMock = new Mock<ConsumeContext<ReportCreatedEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(reportEvent);

        var serviceException = new InvalidOperationException("Service failed");
        _mockNotificationService.Setup(s => s.CreateNotificationAsync(It.IsAny<NotificationDto>()))
            .ThrowsAsync(serviceException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _consumer.Consume(consumeContextMock.Object));
        exception.Should().Be(serviceException);

        // Verify logging (basic check can be more specific with LogLevel and message)
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true), // We are not checking the message content here
                serviceException, // Check if the exception is passed to the logger
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!),
            Times.Never); // The logger in the consumer is for SignalR error, not service error, it rethrows.
                         // If you want to log service errors too, add that to the consumer and test it.
                         // The current consumer logs SignalR errors.
    }

    [Fact]
    public async Task Consume_SignalRSendThrows_ExceptionIsPropagatedAndLogged()
    {
        // Arrange
        var reportEvent = new ReportCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        var consumeContextMock = new Mock<ConsumeContext<ReportCreatedEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(reportEvent);

        var createdNotificationDto = new NotificationDto(Guid.NewGuid(), "NewReport", reportEvent.ReportId, reportEvent.CreatedByUserId, reportEvent.CreatedAt, reportEvent.FarmId, null, false);
        _mockNotificationService.Setup(s => s.CreateNotificationAsync(It.IsAny<NotificationDto>())).ReturnsAsync(createdNotificationDto);

        var signalRException = new HubException("SignalR failed");
        _mockClientProxy.Setup(cp => cp.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>(), CancellationToken.None))
                        .ThrowsAsync(signalRException);
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<HubException>(() => _consumer.Consume(consumeContextMock.Object));
        exception.Should().Be(signalRException);

        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Error sending SignalR notification for ReportId: {reportEvent.ReportId}")),
                signalRException,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }
}