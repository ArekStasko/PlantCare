using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

public class PlaceCacheRepository : IReadPlaceRepository
{
    private readonly IReadPlaceRepository _repository;
    private readonly ILogger<PlaceCacheRepository> _logger;

    public PlaceCacheRepository(IReadPlaceRepository repository, ILogger<PlaceCacheRepository> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public ValueTask<Result<IReadOnlyCollection<IPlace>>> Get()
    {
        throw new NotImplementedException();
    }
}