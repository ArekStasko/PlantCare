using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;
using PlantCare.Persistance.ReadDataManager.Interfaces;

namespace PlantCare.ConsistencyManager.Services;

public class PlantConsistencyService : IQueueConsumer<Plant>
{
    private readonly IPlantReadContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PlantConsistencyService> _logger;

    public PlantConsistencyService(IPlantReadContext context, IMapper mapper, ILogger<PlantConsistencyService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task ConsumeAsync(Plant message)
    {
        switch (message.Action)
        {
            case ActionType.Add:
            {
                var plant = _mapper.Map<PlantCare.Domain.Models.ReadModels.Plant.Plant>(message.PlantData);
                await _context.Plants.AddAsync(plant);
                await _context.SaveChangesAsync();
                return;
            }
            case ActionType.Delete:
            {
                var plantId = message.PlantData.Id;
                var plantToDelete = await _context.Plants.SingleOrDefaultAsync(m => m.ConsistencyId == plantId);

                if (plantToDelete == null)
                {
                    _logger.LogError("There is no plant with {id} id", plantId);
                    return;
                }

                _context.Plants.Remove(plantToDelete);
                await _context.SaveChangesAsync();
                return;
            }
            case ActionType.Update:
            {
                var plant = _mapper.Map<PlantCare.Domain.Models.ReadModels.Plant.Plant>(message.PlantData);
                _context.Plants.Update(plant);
                await _context.SaveChangesAsync();
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
}