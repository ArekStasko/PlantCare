using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Abstraction.Commands.Module;
using PlantCare.Domain.Models.Module;
using PlantCare.Persistance.Interfaces.WriteRepositories;

namespace PlantCare.Commands.CommandHandlers.ModuleCommandHandlers;

public class UpdateModuleHandler : IRequestHandler<UpdateModuleCommand, Result<bool>>
{
    private readonly IWriteModuleRepository _repository;
    private readonly ILogger<UpdateModuleHandler> _logger;
    private readonly IMapper _mapper;
    
    public UpdateModuleHandler(IWriteModuleRepository repository, ILogger<UpdateModuleHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Result<bool>> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("UpdateModuleHandler handles request");
            IModule moduleToUpdate = _mapper.Map<Module>(request);
            var result = await _repository.Update(moduleToUpdate);
            return result.Match(succ =>
            {
                if (succ)
                {
                    _logger.LogInformation("Module was successfully updated");
                    return new Result<bool>(true);
                }

                _logger.LogError("Something went wrong");
                return new Result<bool>(false);
            }, err =>
            {
                _logger.LogError("Error has occured during EditPlaceHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in UpdateModuleHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}