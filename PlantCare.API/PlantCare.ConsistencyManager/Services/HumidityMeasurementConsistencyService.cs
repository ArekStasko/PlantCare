using AutoMapper;
using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;
using PlantCare.Persistance.ReadDataManager.Interfaces;

namespace PlantCare.ConsistencyManager.Services;

public class HumidityMeasurementConsistencyService : IQueueConsumer<HumidityMeasurement>
{
    private readonly IHumidityMeasurementReadContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<HumidityMeasurementConsistencyService> _logger;

    public HumidityMeasurementConsistencyService(IHumidityMeasurementReadContext context, IMapper mapper, ILogger<HumidityMeasurementConsistencyService> logger)
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
                var humidityMeasurement = _mapper.Map<PlantCare.Domain.Models.ReadModels.HumidityMeasurement.HumidityMeasurement>(message.HumidityMeasurementData);
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