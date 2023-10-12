namespace PlantCare.API.Services.QueryHandlers.PlaceQueryHandlers;

using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;
using PlantCare.API.Services.Queries.PlaceQueries;

public class GetPlacesHandler : IRequestHandler<GetPlacesQuery, Result<List<IPlace>>>
{
    private readonly IPlaceRepository _repository;
    private readonly ILogger<GetPlacesHandler> _logger;

    public GetPlacesHandler(IPlaceRepository repository, ILogger<GetPlacesHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<IPlace>>> Handle(GetPlacesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetPlacesHandler handles request");

            var place = await _repository.Get();
            return place.Match(succ =>
            {
                _logger.LogInformation("GetPlacesHandler successfully processed the request");
                return new Result<List<IPlace>>(succ);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetPlacesHandler request");
                return new Result<List<IPlace>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetPlacesHandler: {exception}", e.Message);
            return new Result<List<IPlace>>(e);
        }
    }
}