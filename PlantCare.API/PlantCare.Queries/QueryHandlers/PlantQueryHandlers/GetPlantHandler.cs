using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Plant;
using PlantCare.Queries.Responses.Plants;

namespace PlantCare.Queries.QueryHandlers.PlantQueryHandlers;

public class GetPlantHandler : IRequestHandler<GetPlantQuery, Result<Plant>>
{
    private readonly IReadPlantRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPlantHandler> _logger;

    public GetPlantHandler(IReadPlantRepository repository, IMapper mapper, ILogger<GetPlantHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<Plant>> Handle(GetPlantQuery query, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetPlantHandler handles request");
            var plant = await _repository.Get(query.Id, query.UserId);
            return plant.Match(succ =>
            {
                _logger.LogInformation("GetPlantHandler successfully processed the request");
                var plant = _mapper.Map<Plant>(succ);
                return new Result<Plant>(plant);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetPlantHandler request");
                return new Result<Plant>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetPlantHandler: {exception}", e.Message);
            return new Result<Plant>(e);
        }
    }
}