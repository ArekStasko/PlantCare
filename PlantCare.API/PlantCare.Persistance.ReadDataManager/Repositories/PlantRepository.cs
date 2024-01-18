using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.Interfaces;
using PlantCare.Persistance.Interfaces.ReadRepositories;

namespace PlantCare.Persistance.ReadDataManager.Repositories;

public class PlantRepository : IReadPlantRepository
{
     private readonly IPlantContext _context;
    private readonly ILogger<PlantRepository> _logger;
    public PlantRepository(IPlantContext context, ILogger<PlantRepository> logger)
    {
        _context = context;
        _logger = logger;
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