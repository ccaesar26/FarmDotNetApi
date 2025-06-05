using System.Text;
using ClimateIndicesService.Data;
using ClimateIndicesService.ExternalClients.EdoApiClient;
using ClimateIndicesService.Models.Dtos;
using ClimateIndicesService.Repositories;
using ClimateIndicesService.Services.DroughtAnalysisService;
using ClimateIndicesService.Services.DroughtRecordService;
using ClimateIndicesService.Services.NdviMapService;
using ClimateIndicesService.Services.NdviService;
using ClimateIndicesService.Services.WekeoTokenService;
using MaxRev.Gdal.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.FarmClaimTypes;

GdalBase.ConfigureAll();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new Exception("Jwt:Key not found."));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
builder.Services.AddDbContext<ClimateIndicesDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.UseNetTopologySuite()
    ));

// Register IHttpContextAccessor (required for FarmAuthorizationService)
builder.Services.AddHttpContextAccessor();

// Configuration
builder.Services.Configure<WekeoApiSettings>(builder.Configuration.GetSection("WekeoApiSettings"));
builder.Services.Configure<VitoWmsSettings>(builder.Configuration.GetSection("VitoWmsSettings"));
builder.Services.Configure<VitoWmsNdviSettings>(builder.Configuration.GetSection("VitoWmsNdviSettings"));

// Register services
builder.Services.AddHttpClient<IEdoApiClient, EdoApiClient>();

// HttpClientFactory and typed clients
builder.Services.AddHttpClient("WekeoApiClient"); // Generic client for WEkEO if needed elsewhere
builder.Services.AddHttpClient("VitoWmsClient") // Client for VITO WMS
    .ConfigureHttpClient(client =>
    {
        // You could set a default User-Agent or Timeout here if desired
        // client.Timeout = TimeSpan.FromSeconds(30);
    });

builder.Services.AddScoped<IDroughtRecordRepository, DroughtRecordRepository>();
builder.Services.AddScoped<IDroughtRasterRepository, GdalDroughtRasterRepository>();

builder.Services.AddScoped<IDroughtRecordService, DroughtRecordService>();
builder.Services.AddScoped<IDroughtAnalysisService, DroughtAnalysisService>();

// Services
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IWekeoTokenService, WekeoTokenService>(); // Singleton for token caching logic
builder.Services.AddScoped<INdviService, NdviService>();
builder.Services.AddScoped<INdviMapService, NdviMapService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();