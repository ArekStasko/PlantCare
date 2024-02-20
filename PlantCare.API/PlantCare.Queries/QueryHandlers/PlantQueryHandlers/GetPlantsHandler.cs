using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Plant;
using PlantCare.Queries.Responses.Plants;

namespace PlantCare.Queries.QueryHandlers.PlantQueryHandlers;

public class GetPlantsHandler : IRequestHandler<GetPlantsQuery, Result<IReadOnlyCollection<GetPlantResponse>>>
{
    private readonly IReadPlantRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPlantsHandler> _logger;

    public GetPlantsHandler(IReadPlantRepository repository, IMapper mapper, ILogger<GetPlantsHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyCollection<GetPlantResponse>>> Handle(GetPlantsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetPlantsHandler handles request");

            var plant = await _repository.Get();
            return plant.Match(succ =>
            {
                _logger.LogInformation("GetPlantsHandler successfully processed the request");
                IReadOnlyCollection<GetPlantResponse> plants = succ.Select(plant => _mapper.Map<GetPlantResponse>(plant)).ToList();
                return new Result<IReadOnlyCollection<GetPlantResponse>>(plants);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetPlantsHandler request");
                return new Result<IReadOnlyCollection<GetPlantResponse>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetPlantsHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<GetPlantResponse>>(e);
        }
    }
}