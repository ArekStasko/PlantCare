using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

public class PlaceCacheRepository : IPlaceRepository
{
    private readonly IPlaceRepository _repository;
    private readonly ILogger<PlaceCacheRepository> _logger;
    
    public PlaceCacheRepository(IPlaceRepository repository, ILogger<PlaceCacheRepository> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public ValueTask<Result<bool>> Create(IPlace plant)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<bool>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<bool>> Update(IPlace plant)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<IReadOnlyCollection<IPlace>>> Get()
    {
        throw new NotImplementedException();
    }
}