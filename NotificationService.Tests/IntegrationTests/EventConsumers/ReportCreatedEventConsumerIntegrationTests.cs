// NotificationService.Tests/IntegrationTests/EventConsumers/ReportCreatedEventConsumerIntegrationTests.cs

using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NotificationService.Data;
using NotificationService.EventConsumers;
using NotificationService.Hubs;
using NotificationService.Models.Dtos; // For NotificationDto
using Shared.Models.Events;

namespace NotificationService.Tests.IntegrationTests.EventConsumers;

// Replace 'Program' with your actual startup class if different (e.g. 'Startup')
public class ReportCreatedEventConsumerIntegrationTests : IAsyncLifetime
{
    private ITestHarness _harness;
    private IServiceProvider _provider;
    private NotificationServiceAppFactory<Program> _factory;
    private Mock<IHubContext<NotificationHub>> _mockHubContext;
    private Mock<IClientProxy> _mockClientProxy;

    public async Task InitializeAsync()
    {
        _mockHubContext = new Mock<IHubContext<NotificationHub>>();
        _mockClientProxy = new Mock<IClientProxy>();

        var mockClients = new Mock<IHubClients>();
        mockClients.Setup(clients => clients.Group(It.IsAny<string>())).Returns(_mockClientProxy.Object);
        _mockHubContext.Setup(hc => hc.Clients).Returns(mockClients.Object);

        _factory = new NotificationServiceAppFactory<Program>();
        _factory.AddTestServices(services =>
        {
            // Replace IHubContext with our mock for verification
            services.AddSingleton(_mockHubContext.Object);

            // Add MassTransit TestHarness
            // It will find consumers from the assembly
            services.AddMassTransitTestHarness(cfg => { cfg.AddConsumer<ReportCreatedEventConsumer>(); });
        });

        _provider = _factory.Services; // Get service provider from factory
        _harness = _provider.GetRequiredService<ITestHarness>();

        // Ensure DB is created using the factory's service provider
        using (var scope = _provider.CreateScope()) // _provider is _factory.Services
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<NotificationDbContext>();
            await db.Database.EnsureCreatedAsync();
        }

        await _harness.Start();
    }

    public async Task DisposeAsync()
    {
        await _harness.Stop();
        await _factory.DisposeAsync();
    }

    [Fact]
    public async Task ReportCreatedEvent_ShouldBeConsumed_CreateNotification_And_SendSignalR()
    {
        // Arrange
        var reportEvent = new ReportCreatedEvent(
            Guid.NewGuid(), // ReportId
            Guid.NewGuid(), // FarmId
            Guid.NewGuid(), // CreatedByUserId
            DateTime.UtcNow
        );

        // Act
        await _harness.Bus.Publish(reportEvent);

        // Assert
        // 1. Check if the consumer was called
        (await _harness.Consumed.Any<ReportCreatedEvent>(x => x.Context.Message.ReportId == reportEvent.ReportId))
            .Should().BeTrue();

        // 2. Check if the consumer is registered with the harness
        var consumerHarness = _harness.GetConsumerHarness<ReportCreatedEventConsumer>();
        (await consumerHarness.Consumed.Any<ReportCreatedEvent>(x =>
            x.Context.Message.ReportId == reportEvent.ReportId)).Should().BeTrue();


        // 3. Verify notification created in DB
        using var scope = _provider.CreateScope(); // Use provider from the factory
        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        var notificationInDb = await dbContext.Notifications
            .FirstOrDefaultAsync(n =>
                n.SourceEntityId == reportEvent.ReportId && n.NotificationType == "NewReport");

        notificationInDb.Should().NotBeNull();
        notificationInDb.TargetFarmId.Should().Be(reportEvent.FarmId);
        notificationInDb.TriggeringUserId.Should().Be(reportEvent.CreatedByUserId);
        notificationInDb.Timestamp.Should().Be(reportEvent.CreatedAt);
        notificationInDb.IsRead.Should().BeFalse();

        // 4. Verify SignalR call
        NotificationDto? capturedDto = null; // Variable to store the captured DTO

        // Corrected Setup for SendCoreAsync
        _mockClientProxy
            .Setup(cp => cp.SendCoreAsync(
                "NewReportNotification",
                It.Is<object[]>(args =>
                        args != null &&
                        args.Length == 1 &&
                        args[0] is NotificationDto // Simple type check
                ),
                It.IsAny<CancellationToken>()
            ))
            .Callback<string, object[], CancellationToken>((methodName, args, ct) =>
            {
                // Full pattern matching and assignment CAN be done in the Callback
                if (args.Length == 1 && args[0] is NotificationDto dtoFromCallback)
                {
                    capturedDto = dtoFromCallback;
                }
            })
            .Returns(Task.CompletedTask); // Mock the SendCoreAsync to return a completed task


        // Act
        await _harness.Bus.Publish(reportEvent); // Publish the event, which triggers the consumer

        // Assert
        // ... (other assertions for event consumption and DB state) ...

        // Verification of the SignalR call (optional if callback is primary, but good for confirming it was called once)
        _mockClientProxy.Verify(cp => cp.SendCoreAsync(
                "NewReportNotification",
                It.Is<object[]>(args => // This lambda IS an expression tree
                        args != null &&
                        args.Length == 1 &&
                        args[0] is NotificationDto && // Type check
                        ((NotificationDto)args[0]).SourceEntityId == reportEvent.ReportId && // Cast and access property
                        ((NotificationDto)args[0]).TargetFarmId == reportEvent.FarmId
                    // Add more property checks as needed after casting
                ),
                It.IsAny<CancellationToken>()),
            Times.Once);

        // Assert that the DTO was captured by the callback
        capturedDto.Should()
            .NotBeNull(
                "because SendCoreAsync with the correct parameters should have been called, triggering the callback.");
        if (capturedDto != null) // For type safety if the above assertion could be bypassed by a test runner quirk
        {
            capturedDto.SourceEntityId.Should().Be(reportEvent.ReportId);
            capturedDto.TargetFarmId.Should().Be(reportEvent.FarmId);
            capturedDto.TriggeringUserId.Should().Be(reportEvent.CreatedByUserId);
            capturedDto.NotificationType.Should().Be("NewReport"); // Assuming this is what your consumer sets
        }
    }
}