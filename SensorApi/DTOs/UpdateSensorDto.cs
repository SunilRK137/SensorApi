using System.ComponentModel.DataAnnotations;

namespace SensorApi.DTOs;

public class UpdateSensorDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Unit { get; set; }

    public double? MinValue { get; set; }

    public double? MaxValue { get; set; }

    public double? LastReading { get; set; }

    public DateTimeOffset? LastReadingAt { get; set; }

    public bool IsActive { get; set; }
}