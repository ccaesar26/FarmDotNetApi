using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.FarmClaimTypes;
using WeatherService.Converters;
using WeatherService.Services;

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

// Register IHttpContextAccessor (required for FarmAuthorizationService)
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IWeatherService>();

builder.Services.Configure<IWeatherService>(builder.Configuration);

builder.Services.AddScoped<IWeatherService, WeatherService.Services.WeatherService>();

builder.Services.AddMemoryCache();

// Add controllers
builder.Services
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter()); });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();