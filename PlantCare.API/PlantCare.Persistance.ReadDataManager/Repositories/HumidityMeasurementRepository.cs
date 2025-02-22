using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Persistance.ReadDataManager.Interfaces;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.Repositories;

public class HumidityMeasurementRepository : IReadHumidityMeasurementRepository
{
    private readonly IHumidityMeasurementReadContext _context;
    private readonly ILogger<HumidityMeasurementRepository> _logger;

    public HumidityMeasurementRepository(IHumidityMeasurementReadContext context, ILogger<HumidityMeasurementRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(int id)
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