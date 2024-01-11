using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.DAO.HumidityMeasurement;
using PlantCare.Persistance.Interfaces;

namespace PlantCare.Persistance.WriteDataManager.Repositories.HumidityMeasurementRepository;

public class HumidityMeasurementRepository : IHumidityMeasurementRepository
{
    private readonly IHumidityMeasurementContext _context;
    private readonly ILogger<HumidityMeasurementRepository> _logger;
    private readonly IDistributedCache _cache;

    public HumidityMeasurementRepository(IHumidityMeasurementContext context, ILogger<HumidityMeasurementRepository> logger, IDistributedCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    public async ValueTask<Result<bool>> Add(IHumidityMeasurementDAO humidityMeasurement)
    {
        try
        {
            await _context.HumidityMeasurements.AddAsync((HumidityMeasurementDAO)humidityMeasurement);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Humidity measurement was successfully created");
            await _cache.RemoveAsync($"HumidityMeasurements-{humidityMeasurement.ModuleId}");
            _logger.LogInformation("Cached modules has been removed");
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }
}