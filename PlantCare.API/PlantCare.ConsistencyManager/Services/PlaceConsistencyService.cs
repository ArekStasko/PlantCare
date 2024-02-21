using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class PlaceConsistencyService : IQueueConsumer<Place>
{
    private readonly ILogger<PlaceConsistencyService> _logger;

    public PlaceConsistencyService(ILogger<PlaceConsistencyService> logger)
    {
        _logger = logger;
    }
    public Task ConsumeAsync(Place message)
    {
        _logger.LogInformation("Consuming Place message: {message}", message);
        return Task.CompletedTask;
    }
}