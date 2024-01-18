using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Place;
using PlantCare.Persistance.Interfaces;
using PlantCare.Persistance.Interfaces.WriteRepositories;

namespace PlantCare.Persistance.WriteDataManager.Repositories;

public class PlaceRepository : IWritePlaceRepository
{
        private readonly IPlaceContext _context;
    private readonly ILogger<PlaceRepository> _logger;
    private readonly IDistributedCache _cache;

    public PlaceRepository(IPlaceContext context, ILogger<PlaceRepository> logger, IDistributedCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    public virtual async ValueTask<Result<bool>> Create(IPlace place)
    {
        try
        {
            await _context.Places.AddAsync((Place)place);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully created new place with {placeId} Id", place.Id);
            await _cache.RemoveAsync("Places");
            _logger.LogInformation("Redis cache has been updated");
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public virtual async ValueTask<Result<bool>> Delete(int id)
    {
        try
        {
            var placeToDelete = await _context.Places.SingleOrDefaultAsync(place => place.Id == id);

            if (placeToDelete == null)
            {
                _logger.LogError("There is no place to delete with {placeId} Id", id);
                return new Result<bool>(new NullReferenceException());
            }

            _context.Places.Remove(placeToDelete);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Place with {placeId} successfully deleted", id);
            await _cache.RemoveAsync("Places");
            _logger.LogInformation("Redis cache has been updated");
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public virtual async ValueTask<Result<bool>> Update(IPlace place)
    {
        try
        {
            var placeToEdit = await _context.Places.SingleOrDefaultAsync(plc => plc.Id == place.Id);

            if (placeToEdit == null)
            {
                _logger.LogError("There is no place to update with {placeId} Id", place.Id);
                return new Result<bool>(new NullReferenceException());
            }

            placeToEdit.Name = place.Name;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Place with {placeId} successfully updated", place.Id);
            await _cache.RemoveAsync("Places");
            _logger.LogInformation("Redis cache has been updated");
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }
}