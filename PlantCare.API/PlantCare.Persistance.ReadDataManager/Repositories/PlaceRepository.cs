using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Place;
using PlantCare.Persistance.ReadDataManager.Interfaces;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.Repositories;

public class PlaceRepository : IReadPlaceRepository
{
    private readonly IPlaceReadContext _context;
    private readonly ILogger<PlaceRepository> _logger;

    public PlaceRepository(IPlaceReadContext context, ILogger<PlaceRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public virtual async ValueTask<Result<IReadOnlyCollection<IPlace>>> Get(int userId)
    {
        try
        {
            var places = await _context.Places.Where(p => p.UserId == userId).ToListAsync<IPlace>();
            _logger.LogInformation("Successfull get opreation");
            return new Result<IReadOnlyCollection<IPlace>>(places);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IPlace>>(e);
        }
    }
}