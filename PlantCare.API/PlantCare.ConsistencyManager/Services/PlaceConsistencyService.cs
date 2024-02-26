using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;
using PlantCare.Persistance.ReadDataManager.Interfaces;

namespace PlantCare.ConsistencyManager.Services;

public class PlaceConsistencyService : IQueueConsumer<Place>
{
    private readonly IPlaceReadContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceConsistencyService> _logger;

    public PlaceConsistencyService(IPlaceReadContext context, IMapper mapper, ILogger<PlaceConsistencyService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task ConsumeAsync(Place message)
    {
        switch (message.Action)
        {
            case ActionType.Add:
            {
                var place = _mapper.Map<PlantCare.Domain.Models.ReadModels.Place.Place>(message.PlaceData);
                await _context.Places.AddAsync(place);
                await _context.SaveChangesAsync();
                return;
            }
            case ActionType.Delete:
            {
                var placeId = message.PlaceData.Id;
                var placeToDelete = await _context.Places.SingleOrDefaultAsync(m => m.ConsistencyId == placeId);

                if (placeToDelete == null)
                {
                    _logger.LogError("There is no place with {id} id", placeId);
                    return;
                }
                        
                _context.Places.Remove(placeToDelete);
                await _context.SaveChangesAsync();
                return;
            }
            case ActionType.Update:
            {
                var place = _mapper.Map<PlantCare.Domain.Models.ReadModels.Place.Place>(message.PlaceData);
                _context.Places.Update(place);
                await _context.SaveChangesAsync();
                return;
            }
            default:
            {
                _logger.LogError("Place Consistency service executes for not existing action: {action}", message.Action);
                return;
            }
        }
    }
}