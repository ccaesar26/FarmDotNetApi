// NotificationService.Tests/IntegrationTests/TestAuthHandler.cs

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Shared.Models.FarmClaimTypes;

namespace NotificationService.Tests.IntegrationTests;

public class TestAuthHandlerOptions : AuthenticationSchemeOptions
{
    public Guid? UserId { get; set; }
    public Guid? FarmId { get; set; }
    public string Role { get; set; } = "Manager"; // Default to manager for broad access
}

public class TestAuthHandler(
    IOptionsMonitor<TestAuthHandlerOptions> options,
    ILoggerFactory loggerFactory,
    UrlEncoder encoder
) : AuthenticationHandler<TestAuthHandlerOptions>(options, loggerFactory, encoder)
{
    public const string AuthenticationScheme = "Test";

    private readonly ILogger<TestAuthHandler>
        _logger = loggerFactory.CreateLogger<TestAuthHandler>(); // Initialize logger

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Log the options being used for this authentication attempt
        _logger.LogWarning("TestAuthHandler: Authenticating with Role '{Role}', UserId '{UserId}', FarmId '{FarmId}'",
            Options.Role, Options.UserId, Options.FarmId);

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "TestUser") };
        if (Options.UserId.HasValue)
        {
            // claims.Add(new Claim(ClaimTypes.NameIdentifier, Options.UserId.Value.ToString()));
            claims.Add(new Claim(FarmClaimTypes.UserId, Options.UserId.Value.ToString())); // Use your custom claim type
        }

        if (Options.FarmId.HasValue)
        {
            claims.Add(new Claim(FarmClaimTypes.FarmId, Options.FarmId.Value.ToString())); // Use your custom claim type
        }

        if (!string.IsNullOrEmpty(Options.Role))
        {
            claims.Add(new Claim(FarmClaimTypes.Role, Options.Role)); // This is the standard role claim
        }

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}