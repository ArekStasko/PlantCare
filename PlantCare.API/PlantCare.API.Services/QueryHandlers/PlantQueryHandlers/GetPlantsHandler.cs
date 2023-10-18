namespace PlantCare.API.Services.Handlers;

using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Requests;

public class GetPlantsHandler : IRequestHandler<GetPlantsQuery, Result<IReadOnlyCollection<IPlant>>>
{
    private readonly IPlantRepository _repository;
    private readonly ILogger<GetPlantsHandler> _logger;

    public GetPlantsHandler(IPlantRepository repository, ILogger<GetPlantsHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyCollection<IPlant>>> Handle(GetPlantsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetPlantsHandler handles request");

            var plant = await _repository.Get();
            return plant.Match(succ =>
            {
                _logger.LogInformation("GetPlantsHandler successfully processed the request");
                return new Result<IReadOnlyCollection<IPlant>>(succ);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetPlantsHandler request");
                return new Result<IReadOnlyCollection<IPlant>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetPlantsHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<IPlant>>(e);
        }
    }
}