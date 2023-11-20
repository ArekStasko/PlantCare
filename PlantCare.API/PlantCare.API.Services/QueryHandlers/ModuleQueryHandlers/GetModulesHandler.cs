using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;
using PlantCare.API.Services.Queries.ModuleQueries;

namespace PlantCare.API.Services.QueryHandlers.ModuleQueryHandlers;

public class GetModulesHandler : IRequestHandler<GetModulesQuery, Result<IReadOnlyCollection<IModule>>>
{
    private readonly IReadModuleRepository _repository;
    private readonly ILogger<GetModulesHandler> _logger;

    public GetModulesHandler(IReadModuleRepository repository, ILogger<GetModulesHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyCollection<IModule>>> Handle(GetModulesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetModulesHandler start processing");
            var result = await _repository.Get();
            return result.Match(succ =>
            {
                _logger.LogInformation("Successfully processed GetModulesHandler query handler");
                return new Result<IReadOnlyCollection<IModule>>(succ);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetModulesHandler request");
                return new Result<IReadOnlyCollection<IModule>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetModulesHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<IModule>>(e);
        }
    }
}