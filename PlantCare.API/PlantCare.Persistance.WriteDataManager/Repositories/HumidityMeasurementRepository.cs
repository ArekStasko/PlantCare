using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.WriteDataManager.Repositories;

public class HumidityMeasurementRepository : IWriteHumidityMeasurementRepository
{
    private readonly IHumidityMeasurementWriteContext _context;
    private readonly ILogger<HumidityMeasurementRepository> _logger;

    public HumidityMeasurementRepository(IHumidityMeasurementWriteContext context, ILogger<HumidityMeasurementRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<int>> Add(IHumidityMeasurement humidityMeasurement)
    {
        try
        {
            await _context.HumidityMeasurements.AddAsync((HumidityMeasurement)humidityMeasurement);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Humidity measurement was successfully created");
            return new Result<int>(humidityMeasurement.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<int>(e);
        }
    }
}