namespace SensorApi.DTOs;

public class SensorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
    public double? LastReading { get; set; }
    public DateTimeOffset? LastReadingAt { get; set; }
    public bool IsActive { get; set; }
}