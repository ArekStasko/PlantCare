using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Module;

namespace PlantCare.Queries.QueryHandlers.ModuleQueryHandlers;
public class GetModuleBatteryLevelHandler : IRequestHandler<GetModuleBatteryLevelQuery, Result<int>>
{
    private readonly IReadHumidityMeasurementRepository _repository;
    private readonly ILogger<GetModuleBatteryLevelHandler> _logger;

    public GetModuleBatteryLevelHandler(IReadHumidityMeasurementRepository repository, ILogger<GetModuleBatteryLevelHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Result<int>> Handle(GetModuleBatteryLevelQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.Get(request.ModuleId);
            return result.Match(succ =>
            {
                var latest = succ
                    .OrderByDescending(x => x.MeasurementDate)
                    .FirstOrDefault();

                var latestBatteryLevel = latest?.BatteryLevel;
                return new Result<int>(latestBatteryLevel ?? 0);
            }, err => 
                new Result<int>(err));
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetModuleBatteryLevelHandler: {exception}", e.Message);
            return new Result<int>(e);
        }
    }
}