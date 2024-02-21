using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class PlantConsistencyService : IQueueConsumer<Plant>
{
    private readonly ILogger<PlantConsistencyService> _logger;

    public PlantConsistencyService(ILogger<PlantConsistencyService> logger)
    {
        _logger = logger;
    }
    public Task ConsumeAsync(Plant message)
    {
        _logger.LogInformation("Consuming Plant message: {message}", message);
        return Task.CompletedTask;
    }
}