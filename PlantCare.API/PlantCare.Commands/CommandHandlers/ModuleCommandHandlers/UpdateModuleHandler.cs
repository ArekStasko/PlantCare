using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Module;
using PlantCare.Domain.Models.Module;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;
using Module = PlantCare.MessageBroker.Messages.Module;

namespace PlantCare.Commands.CommandHandlers.ModuleCommandHandlers;

public class UpdateModuleHandler : IRequestHandler<UpdateModuleCommand, Result<bool>>
{
    private readonly IWriteModuleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<Module> _queueProducer;
    private readonly ILogger<UpdateModuleHandler> _logger;
    
    public UpdateModuleHandler(IWriteModuleRepository repository, IMapper mapper, IQueueProducer<Module> queueProducer, ILogger<UpdateModuleHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _queueProducer = queueProducer;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("UpdateModuleHandler handles request");
            IModule moduleToUpdate = _mapper.Map<Domain.Models.Module.Module>(request);
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