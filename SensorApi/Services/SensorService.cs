using Microsoft.EntityFrameworkCore;
using SensorApi.Data;
using SensorApi.DTOs;
using SensorApi.Models;

namespace SensorApi.Services;

public class SensorService : ISensorService
{
    private readonly SensorContext _context;
    private readonly ILogger<SensorService> _logger;

    public SensorService(SensorContext context, ILogger<SensorService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SensorDto>> GetAllSensorsAsync()
    {
        var sensors = await _context.Sensors.ToListAsync();
        return sensors.Select(MapToDto);
    }

    public async Task<SensorDto?> GetSensorByIdAsync(int id)
    {
        var sensor = await _context.Sensors.FindAsync(id);
        return sensor != null ? MapToDto(sensor) : null;
    }

    public async Task<SensorDto> CreateSensorAsync(CreateSensorDto createSensorDto)
    {
        var sensor = new Sensor
        {
            Name = createSensorDto.Name,
            Type = createSensorDto.Type,
            Unit = createSensorDto.Unit,
            MinValue = createSensorDto.MinValue,
            MaxValue = createSensorDto.MaxValue,
            LastReading = createSensorDto.LastReading,
            LastReadingAt = createSensorDto.LastReadingAt,
            IsActive = createSensorDto.IsActive
        };

        _context.Sensors.Add(sensor);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created new sensor with ID {SensorId}", sensor.Id);
        return MapToDto(sensor);
    }

    public async Task<SensorDto?> UpdateSensorAsync(int id, UpdateSensorDto updateSensorDto)
    {
        var sensor = await _context.Sensors.FindAsync(id);
        if (sensor == null)
        {
            return null;
        }

        sensor.Name = updateSensorDto.Name;
        sensor.Type = updateSensorDto.Type;
        sensor.Unit = updateSensorDto.Unit;
        sensor.MinValue = updateSensorDto.MinValue;
        sensor.MaxValue = updateSensorDto.MaxValue;
        sensor.LastReading = updateSensorDto.LastReading;
        sensor.LastReadingAt = updateSensorDto.LastReadingAt;
        sensor.IsActive = updateSensorDto.IsActive;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated sensor with ID {SensorId}", sensor.Id);
        return MapToDto(sensor);
    }

    public async Task<bool> DeleteSensorAsync(int id)
    {
        var sensor = await _context.Sensors.FindAsync(id);
        if (sensor == null)
        {
            return false;
        }

        _context.Sensors.Remove(sensor);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted sensor with ID {SensorId}", id);
        return true;
    }

    public async Task<IEnumerable<SensorDto>> GetActiveSensorsAsync()
    {
        var sensors = await _context.Sensors
            .Where(s => s.IsActive)
            .ToListAsync();
        return sensors.Select(MapToDto);
    }

    public async Task<IEnumerable<SensorDto>> GetSensorsByTypeAsync(string type)
    {
        var sensors = await _context.Sensors
            .Where(s => s.Type.ToLower() == type.ToLower())
            .ToListAsync();
        return sensors.Select(MapToDto);
    }

    private static SensorDto MapToDto(Sensor sensor)
    {
        return new SensorDto
        {
            Id = sensor.Id,
            Name = sensor.Name,
            Type = sensor.Type,
            Unit = sensor.Unit,
            MinValue = sensor.MinValue,
            MaxValue = sensor.MaxValue,
            LastReading = sensor.LastReading,
            LastReadingAt = sensor.LastReadingAt,
            IsActive = sensor.IsActive
        };
    }
}