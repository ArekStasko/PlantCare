using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;
using PlantCare.API.Services.Queries.ModuleQueries;

namespace PlantCare.API.Services.QueryHandlers.ModuleQueryHandlers;

public class GetCurrentMoistureHandler : IRequestHandler<GetCurrentMositureQuery, Result<int>>
{
    private readonly IModuleRepository _repository;
    private readonly ILogger<GetCurrentMoistureHandler> _logger;

    public GetCurrentMoistureHandler(IModuleRepository repository, ILogger<GetCurrentMoistureHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Result<int>> Handle(GetCurrentMositureQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetCurrentMoistureHandler start processing");
            var result = await _repository.Get(request.Id);
            return result.Match(succ =>
            {
                int currentMoisture = succ.HumidityMeasurements.Last().Humidity;
                _logger.LogInformation("GetCurrentMoistureHandler successfully loaded current moisture level : {currentMoisture}", currentMoisture);
                return new Result<int>(currentMoisture);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetCurrentMoistureHandler request");
                return new Result<int>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetModulesHandler: {exception}", e.Message);
            return new Result<int>(e);
        }
    }
}