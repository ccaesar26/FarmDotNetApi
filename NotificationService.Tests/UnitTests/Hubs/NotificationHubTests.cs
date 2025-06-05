// NotificationService.Tests/UnitTests/Hubs/NotificationHubTests.cs

using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NotificationService.Hubs;

namespace NotificationService.Tests.UnitTests.Hubs;

public class NotificationHubTests
{
    private readonly Mock<HubCallerContext> _mockHubCallerContext;
    private readonly Mock<IGroupManager> _mockGroups;
    private readonly NotificationHub _hub;
    private readonly Guid _testFarmId = Guid.NewGuid();

    public NotificationHubTests()
    {
        _mockHubCallerContext = new Mock<HubCallerContext>();
        _mockGroups = new Mock<IGroupManager>();

        _hub = new NotificationHub
        {
            Context = _mockHubCallerContext.Object,
            Groups = _mockGroups.Object
        };
    }

    private void SetupUserWithFarmId(string farmIdValue)
    {
        var claims = new[] { new Claim("farmId", farmIdValue) }; // Ensure claim name matches "farmId"
        var claimsIdentity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        _mockHubCallerContext.Setup(c => c.User).Returns(claimsPrincipal);
        _mockHubCallerContext.Setup(c => c.ConnectionId).Returns("test-connection-id");
    }
    
    private void SetupUserWithoutFarmId()
    {
        _mockHubCallerContext.Setup(c => c.User).Returns(new ClaimsPrincipal(new ClaimsIdentity())); // No farmId claim
        _mockHubCallerContext.Setup(c => c.ConnectionId).Returns("test-connection-id");
    }

    [Fact]
    public async Task OnConnectedAsync_WithFarmIdClaim_AddsToGroup()
    {
        // Arrange
        SetupUserWithFarmId(_testFarmId.ToString());

        // Act
        await _hub.OnConnectedAsync();

        // Assert
        _mockGroups.Verify(g => g.AddToGroupAsync("test-connection-id", $"Farm_{_testFarmId}", CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnConnectedAsync_WithoutFarmIdClaim_ThrowsException()
    {
        // Arrange
        SetupUserWithoutFarmId();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _hub.OnConnectedAsync());
        exception.Message.Should().Be("FarmId not found in user claims.");
    }

    [Fact]
    public async Task OnDisconnectedAsync_WithFarmIdClaim_RemovesFromGroup()
    {
        // Arrange
        // In OnDisconnectedAsync, your code uses "FarmId" (capital F) for the claim.
        // Let's assume it should be consistent. I'll use "farmId" as per OnConnected.
        // If it's truly different, adjust the claim name here.
        var claims = new[] { new Claim("FarmId", _testFarmId.ToString()) }; // Using "FarmId" as in your code
        var claimsIdentity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        _mockHubCallerContext.Setup(c => c.User).Returns(claimsPrincipal);
        _mockHubCallerContext.Setup(c => c.ConnectionId).Returns("test-connection-id");
        
        // Act
        await _hub.OnDisconnectedAsync(null);

        // Assert
        _mockGroups.Verify(g => g.RemoveFromGroupAsync("test-connection-id", $"Farm_{_testFarmId}", CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task OnDisconnectedAsync_WithoutFarmIdClaim_DoesNotRemoveFromGroup()
    {
        // Arrange
        SetupUserWithoutFarmId(); // This sets up User without "FarmId" or "farmId"
        
        // Act
        await _hub.OnDisconnectedAsync(null);

        // Assert
        _mockGroups.Verify(g => g.RemoveFromGroupAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None), Times.Never);
    }


    [Fact]
    public void GetFarmId_WithFarmIdClaim_ReturnsGuid()
    {
        // Arrange
        // GetFarmId in your code uses "FarmId" (capital F).
        var claims = new[] { new Claim("FarmId", _testFarmId.ToString()) };
        var claimsIdentity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        _mockHubCallerContext.Setup(c => c.User).Returns(claimsPrincipal);

        // Act
        var result = _hub.GetFarmId();

        // Assert
        result.Should().Be(_testFarmId);
    }

    [Fact]
    public void GetFarmId_WithoutFarmIdClaim_ThrowsException()
    {
        // Arrange
        SetupUserWithoutFarmId(); // No "FarmId" claim

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _hub.GetFarmId());
        exception.Message.Should().Be("FarmId not found in user claims.");
    }
}