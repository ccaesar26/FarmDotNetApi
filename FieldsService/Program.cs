using System.Text;
using FieldsService.Data;
using FieldsService.Repositories;
using FieldsService.Services;
using FieldsService.Services.GeocodingService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.FarmAuthorizationService;
using Shared.Services.FarmAuthorizationService;

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
builder.Services.AddDbContext<FieldsDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.UseNetTopologySuite()
    ));

// Register IHttpContextAccessor (required for FarmAuthorizationService)
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IGeocodingService>();

builder.Services.Configure<IGeocodingService>(builder.Configuration);

builder.Services.AddScoped<IGeocodingService, GeocodingService>();

// Register FarmAuthorizationService as a Scoped service
builder.Services.AddScoped<IFarmAuthorizationService, FarmAuthorizationService>();

// Add repositories
builder.Services.AddScoped<IFieldsRepository, FieldsRepository>();

// Add services
builder.Services.AddScoped<IFieldsService, FieldsService.Services.FieldsService>();

builder.Services.AddMemoryCache();

// Add controllers
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new NetTopologySuite.IO.Converters.GeometryConverter());
    });
builder.Services.AddEndpointsApiExplorer();

// Add OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();