using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.WriteDataManager.Repositories;

public class PlantRepository : IWritePlantRepository
{
    private readonly IPlantWriteContext _context;
    private readonly ILogger<PlantRepository> _logger;
    private readonly IDistributedCache _cache;
    public PlantRepository(IPlantWriteContext context, ILogger<PlantRepository> logger, IDistributedCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }
    
    public virtual async ValueTask<Result<int>> Create(IPlant plant)
    {
        try
        {
            await _context.Plants.AddAsync((Plant)plant);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully created new plant with {plantId} Id", plant.Id);
            await ResetCacheValues();
            return new Result<int>(plant.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<int>(e);
        }
    }

    public virtual async ValueTask<Result<bool>> Delete(int id)
    {
        try
        {
            var plantToDelete = await _context.Plants.SingleOrDefaultAsync(plant => plant.Id == id);

            if (plantToDelete == null)
            {
                _logger.LogError("There is no plant to delete with {plantId} Id", id);
                return new Result<bool>(new NullReferenceException());
            }

            _context.Plants.Remove(plantToDelete);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Plant with {plantId} successfully deleted", id);
            await ResetCacheValues();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public virtual async ValueTask<Result<bool>> Update(IPlant plant)
    {
        try
        {
            var plantToUpdate = await _context.Plants.SingleOrDefaultAsync(plt => plt.Id == plant.Id);

            if (plantToUpdate == null)
            {
                _logger.LogError("There is no plant to edit with {plantId} Id", plant.Id);
                return new Result<bool>(new NullReferenceException());
            }

            plantToUpdate.Name = plant.Name;
            plantToUpdate.Description = plant.Description;
            plantToUpdate.Type = plant.Type;
            await _context.SaveChangesAsync();
            await ResetCacheValues();
            _logger.LogInformation("Plant with {plantId} successfully updated", plant.Id);
            
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    private async Task ResetCacheValues()
    {
        //TODO: Should i run this tasks in parallel ? 
        await _cache.RemoveAsync("Plants");
        await _cache.RemoveAsync("Modules");
        await _cache.RemoveAsync("Places");
        _logger.LogInformation("Redis cache has been updated");
    }
}