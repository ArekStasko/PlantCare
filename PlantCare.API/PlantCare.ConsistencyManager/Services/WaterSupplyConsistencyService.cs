using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class WaterSupplyConsistencyService(
    IDistributorConsistencyContext context,
    ILogger<WaterSupplyConsistencyService> logger
    ) : IQueueConsumer<WaterSupply>
{
    public async Task ConsumeAsync(WaterSupply message)
    {
        switch (message.Action)
        {
            case ActionType.Add:
            {
                await context.WaterSupplies.AddAsync(message.WaterSupplyDto);
                await context.SaveChangesAsync();
                return;
            }
            default:
            {
                logger.LogError("Distributor Consistency service executes for not existing action: {action}", message.Action);
                return;
            }
        }
    }
}