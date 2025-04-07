using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlantedCropsService.Data;
using PlantedCropsService.Repositories.CropRepository;
using PlantedCropsService.Repositories.FertilizerEventRepository;
using PlantedCropsService.Repositories.GenericRepository;
using PlantedCropsService.Repositories.GrowthStageEventRepository;
using PlantedCropsService.Repositories.HealthStatusEventRepository;
using PlantedCropsService.Repositories.NoteRepository;
using PlantedCropsService.Services.CropService;
using PlantedCropsService.Services.GrowthStageEventService;
using PlantedCropsService.Services.UnitOfWork;
using Shared.FarmAuthorizationService;

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
    .AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"))
    .AddPolicy("WorkerOrManager", policy => policy.RequireRole("Manager", "Worker"));

// Add DbContext
builder.Services.AddDbContext<CropsDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.UseNetTopologySuite()
    ));

// Register IHttpContextAccessor (required for FarmAuthorizationService)
builder.Services.AddHttpContextAccessor();

// Register FarmAuthorizationService
builder.Services.AddScoped<IFarmAuthorizationService, FarmAuthorizationService>();

// Register repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IGrowthStageEventRepository, GrowthStageEventRepository>();
builder.Services.AddScoped<IHealthStatusEventRepository, HealthStatusEventRepository>();
builder.Services.AddScoped<IFertilizerEventRepository, FertilizerEventRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<ICropRepository, CropRepository>();

// Register services
builder.Services.AddScoped<IGrowthStageEventService, GrowthStageEventService>();
// builder.Services.AddScoped<IFertilizerEventService, FertilizerEventService>();
// builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<ICropService, CropService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add controllers
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new NetTopologySuite.IO.Converters.GeometryConverter());
    });
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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