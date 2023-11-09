using AutoMapper;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.DataAccess.Repositories.PlantRepository;

public class PlantRepository : IPlantRepository
{
    private IPlantContext _context;
    private ILogger<IPlantRepository> _logger;
    private IMapper _mapper;

    public PlantRepository(IPlantContext context, ILogger<IPlantRepository> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    
    public virtual async ValueTask<Result<bool>> Create(IPlant plant)
    {
        try
        {
            await _context.Plants.AddAsync((Plant)plant);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully created new plant with {plantId} Id", plant.Id);
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
            var plantToDelete = await _context.Plants.SingleOrDefaultAsync(plant => plant.Id == id);

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
            plantToUpdate.CriticalMoistureLevel = plant.CriticalMoistureLevel;
            plantToUpdate.RequiredMoistureLevel = plant.RequiredMoistureLevel;
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

    public virtual async ValueTask<Result<IReadOnlyCollection<IPlant>>> Get()
    {
        try
        {
            var plants = await _context.Plants.ToListAsync<IPlant>();
            _logger.LogInformation("Successfull get opreation");
            return new Result<IReadOnlyCollection<IPlant>>(plants);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IPlant>>(e);
        }
    }

    public virtual async ValueTask<Result<IPlant>> Get(int id)
    {
        try
        {
            var plant = await _context.Plants.SingleOrDefaultAsync(plt => plt.Id == id);

            if (plant == null)
            {
                _logger.LogError("There is no plant with {plantId} Id", plant.Id);
                return new Result<IPlant>(new NullReferenceException());
            }

            return new Result<IPlant>(plant);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IPlant>(e);
        }
    }
}