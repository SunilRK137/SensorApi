using Microsoft.EntityFrameworkCore;
using SensorApi.Models;

namespace SensorApi.Data;

public class SensorContext : DbContext
{
    public SensorContext(DbContextOptions<SensorContext> options) : base(options)
    {
    }

    public DbSet<Sensor> Sensors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Sensor entity
        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(20);
        });

        // Seed some initial data
        modelBuilder.Entity<Sensor>().HasData(
            new Sensor
            {
                Id = 1,
                Name = "Temperature Sensor",
                Type = "Temperature",
                Unit = "°C",
                MinValue = -40,
                MaxValue = 125,
                LastReading = 22.5,
                LastReadingAt = DateTimeOffset.UtcNow.AddMinutes(-30),
                IsActive = true
            },
            new Sensor
            {
                Id = 2,
                Name = "Humidity Sensor",
                Type = "Humidity",
                Unit = "%",
                MinValue = 0,
                MaxValue = 100,
                LastReading = 65.2,
                LastReadingAt = DateTimeOffset.UtcNow.AddMinutes(-25),
                IsActive = true
            },
            new Sensor
            {
                Id = 3,
                Name = "Pressure Sensor",
                Type = "Pressure",
                Unit = "kPa",
                MinValue = 0,
                MaxValue = 1000,
                LastReading = 101.3,
                LastReadingAt = DateTimeOffset.UtcNow.AddMinutes(-20),
                IsActive = false
            }
        );
    }
}