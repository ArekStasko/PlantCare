using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class PlaceConsistencyService : IQueueConsumer<Place>
{
    private readonly IPlaceConsistencyContext _context;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly ILogger<PlaceConsistencyService> _logger;

    public PlaceConsistencyService(IPlaceConsistencyContext context, IMapper mapper, IDistributedCache cache, ILogger<PlaceConsistencyService> logger)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
    }
    public async Task ConsumeAsync(Place message)
    {
        switch (message.Action)
        {
            case ActionType.Add:
            {
                var place = _mapper.Map<PlantCare.Domain.Models.Place.Place>(message.PlaceData);
                await _context.Places.AddAsync(place);
                await _context.SaveChangesAsync();
                await ResetCachePlaces();
                return;
            }
            case ActionType.Delete:
            {
                var placeId = message.PlaceData.Id;
                var placeToDelete = await _context.Places.SingleOrDefaultAsync(m => m.Id == placeId);

                if (placeToDelete == null)
                {
                    _logger.LogError("There is no place with {id} id", placeId);
                    return;
                }
                        
                _context.Places.Remove(placeToDelete);
                await _context.SaveChangesAsync();
                await ResetCachePlaces();
                return;
            }
            case ActionType.Update:
            {
                var place = _mapper.Map<PlantCare.Domain.Models.Place.Place>(message.PlaceData);
                _context.Places.Update(place);
                await _context.SaveChangesAsync();
                await ResetCachePlaces();
                return;
            }
            default:
            {
                _logger.LogError("Place Consistency service executes for not existing action: {action}", message.Action);
                return;
            }
        }
    }

    private async Task ResetCachePlaces()
    {
        await _cache.RemoveAsync("Places");
        _logger.LogInformation("Places cache has been updated");
    }
}