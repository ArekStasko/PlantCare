using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class PlantConsistencyService : IQueueConsumer<Plant>
{
    private readonly IPlantConsistencyContext _context;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly ILogger<PlantConsistencyService> _logger;

    public PlantConsistencyService(IPlantConsistencyContext context, IMapper mapper, IDistributedCache cache, ILogger<PlantConsistencyService> logger)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
    }

    public async Task ConsumeAsync(Plant message)
    {
        switch (message.Action)
        {
            case ActionType.Add:
            {
                var plant = _mapper.Map<PlantCare.Domain.Models.Plant.Plant>(message.PlantData);
                await _context.Plants.AddAsync(plant);
                await _context.SaveChangesAsync();
                await ResetPlantCache(plant.UserId);
                return;
            }
            case ActionType.Delete:
            {
                var plantId = message.PlantData.Id;
                var plantToDelete = await _context.Plants.SingleOrDefaultAsync(m => m.Id == plantId);

                if (plantToDelete == null)
                {
                    _logger.LogError("There is no plant with {id} id", plantId);
                    return;
                }

                _context.Plants.Remove(plantToDelete);
                await _context.SaveChangesAsync();
                await ResetPlantCache(plantToDelete.UserId);
                string singlePlantKey = $"Plant-{plantId}-{plantToDelete.UserId}";
                await _cache.RemoveAsync(singlePlantKey);
                return;
            }
            case ActionType.Update:
            {
                var plant = _mapper.Map<PlantCare.Domain.Models.Plant.Plant>(message.PlantData);
                var plantToUpdate = await _context.Plants.SingleOrDefaultAsync(plt => plt.Id == plant.Id);
            
                if (plantToUpdate == null)
                {
                    _logger.LogError("There is no plant to edit with {plantId} Id", plant.Id);
                    return;
                }

                plantToUpdate.Name = plant.Name;
                plantToUpdate.Description = plant.Description;
                plantToUpdate.Type = plant.Type;
                await _context.SaveChangesAsync();
                string singlePlantKey = $"Plant-{plant.Id}-{plant.UserId}";
                await ResetPlantCache(plant.UserId);
                await _cache.RemoveAsync(singlePlantKey);
                return;
            }
            default:
            {
                _logger.LogError("Plant Consistency service executes for not existing action: {action}",
                    message.Action);
                return;
            }
        }
    }
    
    private async Task ResetPlantCache(int userId)
    {
        await Task.WhenAll(
            _cache.RemoveAsync($"Plants-{userId}"), 
            _cache.RemoveAsync($"Modules-{userId}"), 
            _cache.RemoveAsync($"Places-{userId}")
        );
        
        _logger.LogInformation("Plant cache has been updated");
    }
}