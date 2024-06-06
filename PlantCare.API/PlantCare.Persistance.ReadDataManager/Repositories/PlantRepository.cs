using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.ReadDataManager.Interfaces;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.Repositories;

public class PlantRepository : IReadPlantRepository
{
     private readonly IPlantReadContext _context;
    private readonly ILogger<PlantRepository> _logger;
    public PlantRepository(IPlantReadContext context, ILogger<PlantRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public virtual async ValueTask<Result<IReadOnlyCollection<IPlant>>> Get(int userId)
    {
        try
        {
            var plants = await _context.Plants.Where(p => p.UserId == userId).ToListAsync<IPlant>();
            _logger.LogInformation("Successfull get opreation");
            return new Result<IReadOnlyCollection<IPlant>>(plants);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IPlant>>(e);
        }
    }

    public virtual async ValueTask<Result<IPlant>> Get(int id, int userId)
    {
        try
        {
            var plant = await _context.Plants.SingleOrDefaultAsync(plt => plt.Id == id && plt.UserId == userId);

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