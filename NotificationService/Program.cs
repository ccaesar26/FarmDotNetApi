using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotificationService.Data;
using NotificationService.EventConsumers;
using NotificationService.Hubs;
using NotificationService.Repositories;
using NotificationService.Services;
using Shared.FarmAuthorizationService;
using Shared.Models.FarmClaimTypes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new Exception("Jwt:Key not found."));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Audience = jwtSettings["Audience"];
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        // Validate the token issuer and audience
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = true,
            ValidateLifetime = true, // Ensures tokens are not expired
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Request.Cookies.TryGetValue("AuthToken", out var token);
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
    });

// Add authorization
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ManagerOnly", policy => policy.RequireClaim(FarmClaimTypes.Role, "Manager"))
    .AddPolicy("WorkerOnly", policy => policy.RequireClaim(FarmClaimTypes.Role, "Worker"))
    .AddPolicy("ManagerAndWorkers", policy => policy.RequireClaim(FarmClaimTypes.Role, "Worker", "Manager"));

// Add DbContext
var useProductionDb = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("USE_INMEMORY_DB_FOR_INTEGRATION_TESTS"));
if (useProductionDb)
{
    // Use production database (e.g., PostgreSQL, SQL Server, etc.)
    builder.Services.AddDbContext<NotificationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}
// builder.Services.AddDbContext<NotificationDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register FarmAuthorizationService as a Scoped service
builder.Services.AddScoped<IFarmAuthorizationService, FarmAuthorizationService>();

// Register IHttpContextAccessor (required for FarmAuthorizationService)
builder.Services.AddHttpContextAccessor();

// Register repositories, services, etc.
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService.Services.NotificationService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200", "http://localhost:5800"); // Angular & Ocelot
    });
});

// Add SignalR
builder.Services.AddSignalR(options => { options.EnableDetailedErrors = true; });

// Add MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReportCreatedEventConsumer>();
    x.AddConsumer<ReportNewCommentEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], builder.Configuration["RabbitMq:VirtualHost"],
            h =>
            {
                h.Username(builder.Configuration["RabbitMq:Username"] ?? throw new InvalidOperationException());
                h.Password(builder.Configuration["RabbitMq:Password"] ?? throw new InvalidOperationException());
            });

        // Configure receive endpoints for your consumers
        // It's good practice to have a unique queue name for each service's consumers
        cfg.ReceiveEndpoint("notification-service-report-created", e => // Unique queue name
        {
            e.ConfigureConsumer<ReportCreatedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("notification-service-report-new-comment", e => // Unique queue name
        {
            e.ConfigureConsumer<ReportNewCommentEventConsumer>(context);
        });

        cfg.ConfigureEndpoints(context); // Automatically configures endpoints based on consumers
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

// app.UseHttpsRedirection();

app.UseRouting(); // Routing must come before Authentication and Authorization

app.UseCors("CorsPolicy"); // Apply CORS policy *before* Routing, Authentication, and Authorization

app.UseAuthentication(); // IMPORTANT: For SignalR [Authorize] on Hub and accessing Context.User
app.UseAuthorization();

app.MapControllers(); // If you have API controllers

// Map the SignalR Hub endpoint
app.UseEndpoints(endpoints =>
{
    endpoints
        .MapHub<NotificationHub>("/hubs/notifications")
        .RequireCors("CorsPolicy"); // Apply CORS policy to SignalR Hub
});

app.Run();

public partial class Program
{
} // Add public keyword and make it partial