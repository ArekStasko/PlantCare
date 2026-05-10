using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Place;
using PlantCare.Queries.Responses.Place;

namespace PlantCare.Queries.QueryHandlers.PlaceQueryHandlers;

public class PlaceQueryHandler : IRequestHandler<GetPlacesQuery, Result<IReadOnlyCollection<Place>>>
{
    private readonly IReadPlaceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceQueryHandler> _logger;

    public PlaceQueryHandler(IReadPlaceRepository repository, IMapper mapper, ILogger<PlaceQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyCollection<Place>>> Handle(GetPlacesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var place = await _repository.Get(query.UserId);
            return place.Match(succ =>
            {
                IReadOnlyCollection<Place> places = succ.Select(place => _mapper.Map<Place>(place)).ToList();
                return new Result<IReadOnlyCollection<Place>>(places);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetPlacesHandler request");
                return new Result<IReadOnlyCollection<Place>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetPlacesHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<Place>>(e);
        }
    }
}