using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.WriteDataManager.Repositories;

public class PlantRepository : IWritePlantRepository
{
    private readonly IPlantWriteContext _context;
    private readonly ILogger<PlantRepository> _logger;
    public PlantRepository(IPlantWriteContext context, ILogger<PlantRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public virtual async ValueTask<Result<int>> Create(IPlant plant)
    {
        try
        {
            await _context.Plants.AddAsync((Plant)plant);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully created new plant with {plantId} Id", plant.Id);
            return new Result<int>(plant.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<int>(e);
        }
    }

    public virtual async ValueTask<Result<bool>> Delete(int id, int userId)
    {
        try
        {
            var plantToDelete = await _context.Plants.SingleOrDefaultAsync(plant => plant.Id == id && plant.UserId == userId);

            if (plantToDelete == null)
            {
                _logger.LogError("There is no plant to delete with {plantId} Id", id);
                return new Result<bool>(new NullReferenceException());
            }

            _context.Plants.Remove(plantToDelete);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Plant with {plantId} successfully deleted", id);
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
            plantToUpdate.ModuleId = plant.ModuleId;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Plant with {plantId} successfully updated", plant.Id);
            
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }
}