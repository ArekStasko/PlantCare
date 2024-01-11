using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.DAO.HumidityMeasurement;
using PlantCare.Persistance.Interfaces;
using PlantCare.Persistance.Interfaces.ReadRepositories;

namespace PlantCare.Persistance.ReadDataManager.Repositories;

public class HumidityMeasurementRepository : IReadHumidityMeasurementRepository
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

    public async ValueTask<Result<IReadOnlyCollection<IHumidityMeasurementDAO>>> Get(Guid id)
    {
        try
        {
            var humidityMeasurements = await _context.HumidityMeasurements.Where(hm => hm.ModuleId == id).ToListAsync<IHumidityMeasurementDAO>();

            if (humidityMeasurements == null)
            {
                _logger.LogError("There is no humidity measurements for module with {Id} id", id);
                return new Result<IReadOnlyCollection<IHumidityMeasurementDAO>>(new NullReferenceException());
            }

            _logger.LogInformation("Successfully loaded humidity measurements for {Id} module", id);
            return new Result<IReadOnlyCollection<IHumidityMeasurementDAO>>(humidityMeasurements);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IHumidityMeasurementDAO>>(e);
        }
    }
}