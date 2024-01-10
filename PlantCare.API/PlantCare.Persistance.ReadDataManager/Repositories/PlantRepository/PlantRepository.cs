using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.DAO.Plant;
using PlantCare.Persistance.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.Repositories.PlantRepository;

public class PlantRepository : IPlantRepository
{
     private readonly IPlantContext _context;
    private readonly ILogger<PlantRepository> _logger;
    public PlantRepository(IPlantContext context, ILogger<PlantRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public virtual async ValueTask<Result<IReadOnlyCollection<IPlantDAO>>> Get()
    {
        try
        {
            var plants = await _context.Plants.ToListAsync<IPlantDAO>();
            _logger.LogInformation("Successfull get opreation");
            return new Result<IReadOnlyCollection<IPlantDAO>>(plants);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IPlantDAO>>(e);
        }
    }

    public virtual async ValueTask<Result<IPlantDAO>> Get(int id)
    {
        try
        {
            var plant = await _context.Plants.SingleOrDefaultAsync(plt => plt.Id == id);

            if (plant == null)
            {
                _logger.LogError("There is no plant with {plantId} Id", plant.Id);
                return new Result<IPlantDAO>(new NullReferenceException());
            }

            return new Result<IPlantDAO>(plant);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IPlantDAO>(e);
        }
    }

}