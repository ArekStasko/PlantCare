using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Place;
using PlantCare.Persistance.Interfaces.ReadRepositories;
using PlantCare.Queries.Abstraction.Queries.Place;

namespace PlantCare.Queries.QueryHandlers.PlaceQueryHandlers;

public class PlaceQueryHandler : IRequestHandler<GetPlacesQuery, Result<IReadOnlyCollection<IPlace>>>
{
    private readonly IReadPlaceRepository _repository;
    private readonly ILogger<PlaceQueryHandler> _logger;

    public GetPlacesHandler(IReadPlaceRepository repository, ILogger<PlaceQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyCollection<IPlace>>> Handle(GetPlacesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetPlacesHandler handles request");

            var place = await _repository.Get();
            return place.Match(succ =>
            {
                _logger.LogInformation("GetPlacesHandler successfully processed the request");
                return new Result<IReadOnlyCollection<IPlace>>(succ);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetPlacesHandler request");
                return new Result<IReadOnlyCollection<IPlace>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetPlacesHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<IPlace>>(e);
        }
    }
}