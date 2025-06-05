using Microsoft.EntityFrameworkCore;
using NotificationService.Models.Entities;

namespace NotificationService.Data;

public class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : DbContext(options)
{
    public DbSet<NotificationEntity> Notifications { get; set; }
}