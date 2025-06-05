using System.Text;
using CropIdService.Data;
using CropIdService.Repositories.CropHealthRepository;
using CropIdService.Repositories.IdEntryRepository;
using CropIdService.Services.CropHealthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.FarmAuthorizationService;
using Shared.Models.FarmClaimTypes;

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
builder.Services.AddDbContext<CropIdDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register IHttpContextAccessor (required for FarmAuthorizationService)
builder.Services.AddHttpContextAccessor();

// Register FarmAuthorizationService as a Scoped service
builder.Services.AddScoped<IFarmAuthorizationService, FarmAuthorizationService>();

// Add HttpClient
builder.Services.AddHttpClient<ICropHealthRepository>();

// Add configuration for ICropHealthRepository
builder.Services.Configure<ICropHealthRepository>(builder.Configuration);

// Add Repositories
builder.Services.AddScoped<IIdEntryRepository, IdEntryRepository>();
builder.Services.AddScoped<ICropHealthRepository, CropHealthRepository>();

// Add Services
builder.Services.AddScoped<ICropHealthService, CropHealthService>();

builder.Services.AddControllers();

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
builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

// app.UseEndpoints(endpoints => { endpoints.MapHub<WeatherHub>("/cropIdHub"); });

app.Run();