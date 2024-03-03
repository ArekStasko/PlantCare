using AutoMapper;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class HumidityMeasurementConsistencyService : IQueueConsumer<HumidityMeasurement>
{
    private readonly IHumidityMeasurementsConsistencyContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<HumidityMeasurementConsistencyService> _logger;

    public HumidityMeasurementConsistencyService(IHumidityMeasurementsConsistencyContext context, IMapper mapper, ILogger<HumidityMeasurementConsistencyService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task ConsumeAsync(HumidityMeasurement message)
    {
        switch (message.Action)
        {
            case ActionType.Add:
            {
                var humidityMeasurement = _mapper.Map<PlantCare.Domain.Models.HumidityMeasurement.HumidityMeasurement>(message.HumidityMeasurementData);
                await _context.HumidityMeasurements.AddAsync(humidityMeasurement);
                await _context.SaveChangesAsync();
                return;
            }
            default:
            {
                _logger.LogError("Humidity Measurement Consistency service executes for not existing action: {action}", message.Action);
                return;
            }
        }
    }
}