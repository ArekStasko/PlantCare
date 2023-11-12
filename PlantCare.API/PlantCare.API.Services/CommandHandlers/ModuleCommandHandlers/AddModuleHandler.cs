using AutoMapper;
using Castle.Core.Logging;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;
using PlantCare.API.Services.Requests.ModuleCommands;

namespace PlantCare.API.Services.CommandHandlers.ModuleCommandHandlers;

public class AddModuleHandler : IRequestHandler<AddModuleCommand, Result<bool>>
{
    private readonly IModuleRepository _repository;
    private readonly ILogger<AddModuleHandler> _logger;

    public AddModuleHandler(IModuleRepository repository, ILogger<AddModuleHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(AddModuleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("AddModuleHandler start processing");
            var result = await _repository.Add(request.Id);
            return result.Match(succ =>
            {
                if (succ)
                {
                    _logger.LogInformation("Successfully added module with {id} id", request.Id);
                    return new Result<bool>(true);
                }
                
                _logger.LogInformation("Something went wrong");
                return new Result<bool>(new Exception("Something went wrong"));
            }, err =>
            {
                _logger.LogError("Error has occured during AddModuleHandler handling: {exception}", err.Message);
                return new Result<bool>(false);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in AddModuleHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}