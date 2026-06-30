using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class DistributorConsistencyService(
    IDistributorConsistencyContext context,
    IDistributedCache cache,
    ILogger<DistributorConsistencyService> logger,
    IMapper mapper) : IQueueConsumer<Distributor>
{
    public async Task ConsumeAsync(Distributor message)
    {
        switch (message.Action)
                {
                    case ActionType.Add:
                    {
                        var distributor = mapper.Map<PlantCare.Domain.Models.Distributor.Distributor>(message.DistributorDto);
                        await context.Distributors.AddAsync(distributor);
                        await context.SaveChangesAsync();
                        await ResetCacheDistributor(distributor.UserId);
                        return;
                    }
                    case ActionType.WaterSupply:
                    {
                        var distributor = context.Distributors.FirstOrDefault(x => x.UserId == message.DistributorDto.UserId && x.Id == message.DistributorDto.Id);
                        if (distributor == null)
                        {
                            logger.LogError("Distributor with id: {id} does not exists", message.DistributorDto.Id);
                            return;
                        }
                        distributor.WaterSupply = true;
                        await context.SaveChangesAsync();
                        await ResetCacheDistributor(distributor.UserId);
                        return;
                    }
                    default:
                    {
                        logger.LogError("Distributor Consistency service executes for not existing action: {action}", message.Action);
                        return;
                    }
                }
    }

    private async Task ResetCacheDistributor(int userId)
    {
        await cache.RemoveAsync($"distributors_{userId}");
        await cache.RemoveAsync($"distributor_{userId}");
        logger.LogInformation("Redis cache has been updated");
    }
}