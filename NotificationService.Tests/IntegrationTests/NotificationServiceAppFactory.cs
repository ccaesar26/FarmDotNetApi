// NotificationService.Tests/IntegrationTests/NotificationServiceAppFactory.cs


using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Data;
using Shared.FarmAuthorizationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Shared.Models.FarmClaimTypes;

namespace NotificationService.Tests.IntegrationTests
{
    public class NotificationServiceAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        // public Guid TestUserId { get; set; } = Guid.NewGuid();
        // public Guid TestFarmId { get; set; } = Guid.NewGuid();
        // public string TestUserRole { get; set; } = "Manager"; // Default role for tests

        private readonly string _dbName = $"InMemoryDbForTesting_{Guid.NewGuid()}";

        public Action<IServiceCollection>? AddTestServicesAction { get; private set; }

        public NotificationServiceAppFactory()
        {
            Environment.SetEnvironmentVariable("USE_INMEMORY_DB_FOR_INTEGRATION_TESTS", "true");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // DbContext removal 
                var dbContextOptionsDescriptors = services
                    .Where(d => d.ServiceType == typeof(DbContextOptions<NotificationDbContext>)).ToList();
                foreach (var descriptor in dbContextOptionsDescriptors)
                {
                    services.Remove(descriptor);
                }
                
                var dbContextDescriptors = services
                    .Where(d => d.ServiceType == typeof(NotificationDbContext)).ToList();
                foreach (var descriptor in dbContextDescriptors)
                {
                    services.Remove(descriptor);
                }
                
                var dbContextFactoryDescriptors = services
                    .Where(d => d.ServiceType == typeof(IDbContextFactory<NotificationDbContext>)).ToList();
                foreach (var descriptor in dbContextFactoryDescriptors)
                {
                    services.Remove(descriptor);
                }

                // Add InMemory database for testing
                services.AddDbContext<NotificationDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                    Console.WriteLine($"INFO (Factory): Configuring InMemory DB: {_dbName}");
                }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

                // Replace the real IFarmAuthorizationService
                services.AddHttpContextAccessor(); // Ensure IHttpContextAccessor is registered
                services.AddScoped<IFarmAuthorizationService, ClaimsBasedFarmAuthorizationService>();


                // Setup Test Authentication
                services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                    .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options =>
                    {
                        // Set some sensible defaults or leave them null to be configured by CreateClientWithAuth
                        options.Role = "DefaultTestRole";
                        options.UserId = Guid.NewGuid();
                        options.FarmId = Guid.NewGuid();
                    });
                
                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders(); // <<<< CRITICAL: Remove default providers (like EventLog)
                    loggingBuilder.AddConsole();     // Add only the console logger (or others you need for test output)
                    // You can also add Debug logger, etc.
                    // loggingBuilder.AddDebug();

                    // Set minimum log level for console output during tests
                    loggingBuilder.SetMinimumLevel(LogLevel.Warning); // Or Information/Debug as needed for troubleshooting
                    // Example: Filter MassTransit logs if they are too noisy during tests, unless debugging MT
                    // loggingBuilder.AddFilter("MassTransit", LogLevel.Warning);
                });

                // Inject any test services (mocks, consumers, test harness)
                AddTestServicesAction?.Invoke(services);
            });
        }

        // Helper method to create a client with specific auth options
        public HttpClient CreateClientWithAuth(TestAuthHandlerOptions authOptions)
        {
            return this.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => // Use ConfigureTestServices for test-specific overrides
                {
                    // Configure/Override the TestAuthHandlerOptions for this specific client's host
                    services.Configure<TestAuthHandlerOptions>(TestAuthHandler.AuthenticationScheme, options =>
                    {
                        options.UserId = authOptions.UserId;
                        options.FarmId = authOptions.FarmId;
                        options.Role = authOptions.Role;
                    });
                });
            }).CreateClient();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clear the environment variable after the factory is disposed.
                Environment.SetEnvironmentVariable("USE_INMEMORY_DB_FOR_INTEGRATION_TESTS", null);
            }
            base.Dispose(disposing);
        }

        public void AddTestServices(Action<IServiceCollection> configure)
        {
            AddTestServicesAction = configure;
        }

        // Simple IFarmAuthorizationService implementation that reads from claims
        // This could also be your existing Shared.FarmAuthorizationService.DefaultFarmAuthorizationService
        // if it correctly reads from HttpContext.User.Claims
        private class ClaimsBasedFarmAuthorizationService(IHttpContextAccessor httpContextAccessor)
            : IFarmAuthorizationService
        {
            public Guid? GetFarmId()
            {
                var farmIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(FarmClaimTypes.FarmId)?.Value;
                return Guid.TryParse(farmIdClaim, out var farmId) ? farmId : null;
            }

            public Guid? GetUserId()
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(FarmClaimTypes.UserId)?.Value ??
                                  httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;
                return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
            }

            public string GetUserRole() =>
                httpContextAccessor.HttpContext?.User.FindFirst(FarmClaimTypes.Role)?.Value ?? string.Empty;

            public bool IsManager() => GetUserRole() == "Manager";
            public bool IsWorker() => GetUserRole() == "Worker";
        }
    }
}