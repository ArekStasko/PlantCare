using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class HumidityMeasurementConsistencyService : IQueueConsumer<HumidityMeasurement>
{
    private readonly ILogger<HumidityMeasurementConsistencyService> _logger;

    public HumidityMeasurementConsistencyService(ILogger<HumidityMeasurementConsistencyService> logger)
    {
        _logger = logger;
    }
    public Task ConsumeAsync(HumidityMeasurement message)
    {
        return Task.CompletedTask;
    }
}