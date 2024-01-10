using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.DAO.Place;
using PlantCare.Persistance.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.Repositories.PlaceRepository;

public class PlaceRepository : IPlaceRepository
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
    
    public virtual async ValueTask<Result<IReadOnlyCollection<IPlaceDAO>>> Get()
    {
        try
        {
            var places = await _context.Places.ToListAsync<IPlaceDAO>();
            _logger.LogInformation("Successfull get opreation");
            return new Result<IReadOnlyCollection<IPlaceDAO>>(places);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IPlaceDAO>>(e);
        }
    }
}