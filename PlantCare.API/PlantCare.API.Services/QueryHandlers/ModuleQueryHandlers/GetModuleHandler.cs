using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;
using PlantCare.API.Services.Queries.ModuleQueries;

namespace PlantCare.API.Services.QueryHandlers.ModuleQueryHandlers;

public class GetModuleHandler : IRequestHandler<GetModuleQuery, Result<IModule>>
{
    private readonly IModuleRepository _repository;
    private readonly ILogger<GetModuleHandler> _logger;

    public GetModuleHandler(IModuleRepository repository, ILogger<GetModuleHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<IModule>> Handle(GetModuleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetModuleHandler start processing");
            var result = await _repository.Get(request.Id);
            return result.Match(succ =>
            {
                _logger.LogInformation("Successfully processed GetModuleHandler query handler");
                return new Result<IModule>(succ);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetModuleHandler request");
                return new Result<IModule>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetModuleHandler: {exception}", e.Message);
            return new Result<IModule>(e);
        }
    }
}