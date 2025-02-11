using ConfigurationService.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Data;

public class ConfigDbContext(DbContextOptions<ConfigDbContext> options) : DbContext(options)
{
    public DbSet<ConfigSetting> ConfigSettings { get; set; }
}