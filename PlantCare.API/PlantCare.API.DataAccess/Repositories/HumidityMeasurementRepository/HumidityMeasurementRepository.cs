using System.Collections.ObjectModel;
using Castle.Core.Logging;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;

namespace PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;

public class HumidityMeasurementRepository : IWriteHumidityMeasurementRepository, IReadHumidityMeasurementRepository
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

    public async ValueTask<Result<bool>> Add(IHumidityMeasurement humidityMeasurement)
    {
        try
        {
            await _context.HumidityMeasurements.AddAsync((HumidityMeasurement)humidityMeasurement);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Humidity measurement was successfully created");
            await _cache.RemoveAsync("Modules");
            _logger.LogInformation("Cached modules has been removed");
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public async ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(Guid id)
    {
        try
        {
            var humidityMeasurements = await _context.HumidityMeasurements.Where(hm => hm.ModuleId == id).ToListAsync<IHumidityMeasurement>();

            if (humidityMeasurements == null)
            {
                _logger.LogError("There is no humidity measurements for module with {Id} id", id);
                return new Result<IReadOnlyCollection<IHumidityMeasurement>>(new NullReferenceException());
            }

            _logger.LogInformation("Successfully loaded humidity measurements for {Id} module", id);
            return new Result<IReadOnlyCollection<IHumidityMeasurement>>(humidityMeasurements);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IHumidityMeasurement>>(e);
        }
    }
}