using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.FarmAuthorizationService;
using Shared.FarmClaimTypes;
using UserProfileService.Converters;
using UserProfileService.Data;
using UserProfileService.Repositories;
using UserProfileService.Repositories.AttributeCategoryRepository;
using UserProfileService.Repositories.ProfileAttributeRepository;
using UserProfileService.Repositories.UserProfileRepository;
using UserProfileService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new Exception("Jwt:Key not found."));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Audience = builder.Configuration["Auth:Audience"];
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
builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("ManagerOnly", policy => policy.RequireClaim(FarmClaimTypes.Role, "Manager"))
    .AddPolicy("ManagerAndWorkers", policy => policy.RequireClaim(FarmClaimTypes.Role, "Worker", "Manager"));

// Add DbContext
builder.Services.AddDbContext<UserProfileDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register IHttpContextAccessor (required for FarmAuthorizationService)
builder.Services.AddHttpContextAccessor();

// Register FarmAuthorizationService as a Scoped service
builder.Services.AddScoped<IFarmAuthorizationService, FarmAuthorizationService>();

// Add Repositories
builder.Services.AddScoped<IAttributeCategoryRepository, AttributeCategoryRepository>();
builder.Services.AddScoped<IProfileAttributeRepository, ProfileAttributeRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

// Add Services
builder.Services.AddScoped<IUserProfileService, UserProfileService.Services.UserProfileService>();

// Add controllers
builder.Services
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter()); });
builder.Services.AddEndpointsApiExplorer();

// Add MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], builder.Configuration["RabbitMq:VirtualHost"],
            h =>
            {
                h.Username(builder.Configuration["RabbitMq:Username"] ?? throw new InvalidOperationException());
                h.Password(builder.Configuration["RabbitMq:Password"] ?? throw new InvalidOperationException());
            });
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();