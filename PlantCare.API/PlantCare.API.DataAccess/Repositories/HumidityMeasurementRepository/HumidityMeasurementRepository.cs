using Castle.Core.Logging;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;

namespace PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;

public class HumidityMeasurementRepository : IHumidityMeasurementRepository
{
    private readonly IHumidityMeasurementContext _context;
    private readonly ILogger<IHumidityMeasurementRepository> _logger;

    public HumidityMeasurementRepository(IHumidityMeasurementContext context, ILogger<IHumidityMeasurementRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async ValueTask<Result<bool>> Add(IHumidityMeasurement humidityMeasurement)
    {
        try
        {
            await _context.HumidityMeasurements.AddAsync((HumidityMeasurement)humidityMeasurement);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Humidity measurement was successfully created");
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }
}