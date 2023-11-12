using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;
using PlantCare.API.Services.Requests.ModuleCommands;

namespace PlantCare.API.Services.CommandHandlers.ModuleCommandHandlers;

public class DeleteModuleHandler : IRequestHandler<DeleteModuleCommand, Result<bool>>
{
    private readonly IModuleRepository _repository;
    private readonly ILogger<DeleteModuleHandler> _logger;

    public DeleteModuleHandler(IModuleRepository repository, ILogger<DeleteModuleHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.Delete(request.Id);
            return result.Match(succ =>
            {
                if (succ)
                {
                    _logger.LogInformation("Module was successfully deleted");
                    return new Result<bool>(true);
                }

                _logger.LogError("Something went wrong");
                return new Result<bool>(false);
            }, err =>
            {
                _logger.LogError("Error has occured during DeleteModuleHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in DeleteModuleHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}