using SensorApi.DTOs;
using SensorApi.Models;

namespace SensorApi.Services;

public interface ISensorService
{
    Task<IEnumerable<SensorDto>> GetAllSensorsAsync();
    Task<SensorDto?> GetSensorByIdAsync(int id);
    Task<SensorDto> CreateSensorAsync(CreateSensorDto createSensorDto);
    Task<SensorDto?> UpdateSensorAsync(int id, UpdateSensorDto updateSensorDto);
    Task<bool> DeleteSensorAsync(int id);
    Task<IEnumerable<SensorDto>> GetActiveSensorsAsync();
    Task<IEnumerable<SensorDto>> GetSensorsByTypeAsync(string type);
}