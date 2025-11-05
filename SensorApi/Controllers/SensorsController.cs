using Microsoft.AspNetCore.Mvc;
using SensorApi.DTOs;
using SensorApi.Services;

namespace SensorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorsController : ControllerBase
{
    private readonly ISensorService _sensorService;
    private readonly ILogger<SensorsController> _logger;

    public SensorsController(ISensorService sensorService, ILogger<SensorsController> logger)
    {
        _sensorService = sensorService;
        _logger = logger;
    }

    /// <summary>
    /// Get all sensors
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SensorDto>>> GetSensors()
    {
        _logger.LogInformation("Getting all sensors");
        var sensors = await _sensorService.GetAllSensorsAsync();
        return Ok(sensors);
    }

    /// <summary>
    /// Get a specific sensor by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SensorDto>> GetSensor(int id)
    {
        var sensor = await _sensorService.GetSensorByIdAsync(id);
        if (sensor == null)
        {
            return NotFound($"Sensor with ID {id} not found.");
        }

        return Ok(sensor);
    }

    /// <summary>
    /// Create a new sensor
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SensorDto>> CreateSensor(CreateSensorDto createSensorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var sensor = await _sensorService.CreateSensorAsync(createSensorDto);
        return CreatedAtAction(nameof(GetSensor), new { id = sensor.Id }, sensor);
    }

    /// <summary>
    /// Update an existing sensor
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<SensorDto>> UpdateSensor(int id, UpdateSensorDto updateSensorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var sensor = await _sensorService.UpdateSensorAsync(id, updateSensorDto);
        if (sensor == null)
        {
            return NotFound($"Sensor with ID {id} not found.");
        }

        return Ok(sensor);
    }

    /// <summary>
    /// Delete a sensor
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSensor(int id)
    {
        var result = await _sensorService.DeleteSensorAsync(id);
        if (!result)
        {
            return NotFound($"Sensor with ID {id} not found.");
        }

        return NoContent();
    }

    /// <summary>
    /// Get only active sensors
    /// </summary>
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<SensorDto>>> GetActiveSensors()
    {
        var sensors = await _sensorService.GetActiveSensorsAsync();
        return Ok(sensors);
    }

    /// <summary>
    /// Get sensors by type
    /// </summary>
    [HttpGet("type/{type}")]
    public async Task<ActionResult<IEnumerable<SensorDto>>> GetSensorsByType(string type)
    {
        var sensors = await _sensorService.GetSensorsByTypeAsync(type);
        return Ok(sensors);
    }
}