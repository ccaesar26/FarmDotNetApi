using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReportsService.Data;
using ReportsService.Repositories.ReportCommentRepository;
using ReportsService.Repositories.ReportRepository;
using ReportsService.Services.ReportHub;
using ReportsService.Services.ReportService;
using Shared.FarmAuthorizationService;
using Shared.FarmClaimTypes;

var builder = WebApplication.CreateBuilder(args);

// Configure Supabase client
var supabaseSection = builder.Configuration.GetSection("Supabase");
var supabaseUrl = supabaseSection["Url"] ??
                  throw new InvalidOperationException("Supabase URL is missing in configuration.");
var supabaseKey = supabaseSection["Key"] ??
                  throw new InvalidOperationException("Supabase Key is missing in configuration.");

builder.Services.AddSingleton(_ => new Supabase.Client(supabaseUrl, supabaseKey, new Supabase.SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
}));

// Add services to the container.
// Add authentication & authorization
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
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
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
    .AddPolicy("ManagerAndWorkers", policy => policy.RequireClaim(FarmClaimTypes.Role, "Manager", "Worker"));

// Add DbContext
builder.Services.AddDbContext<ReportsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register FarmAuthorizationService as a Scoped service
builder.Services.AddScoped<IFarmAuthorizationService, FarmAuthorizationService>();

// Register IHttpContextAccessor (required for FarmAuthorizationService)
builder.Services.AddHttpContextAccessor();

// Register Repositories
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportCommentRepository, ReportCommentRepository>();

// Register Service
builder.Services.AddScoped<IReportService, ReportService>();

// Register controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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
    x.UsingRabbitMq((_, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], builder.Configuration["RabbitMq:VirtualHost"],
            h =>
            {
                h.Username(builder.Configuration["RabbitMq:Username"] ?? throw new InvalidOperationException());
                h.Password(builder.Configuration["RabbitMq:Password"] ?? throw new InvalidOperationException());
            });
    });
});

var app = builder.Build();

app.UseRouting();

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints => { endpoints.MapHub<ReportHub>("/reportHub"); });

app.Run();