using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Module;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Commands.CommandHandlers.ModuleCommandHandlers;

public class DeleteModuleHandler : IRequestHandler<DeleteModuleCommand, Result<bool>>
{
    private readonly IWriteModuleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<Module> _queueProducer;
    private readonly ILogger<DeleteModuleHandler> _logger;

    public DeleteModuleHandler(IWriteModuleRepository repository, IMapper mapper, IQueueProducer<Module> queueProducer, ILogger<DeleteModuleHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _queueProducer = queueProducer;
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
                    var moduleToSend = new PlantCare.Domain.Models.Module.Module() { Id = request.Id };
                    var moduleMessage = new Module()
                    {
                        Action = ActionType.Delete,
                        ModuleData = _mapper.Map<ModuleDto>(moduleToSend)
                    };
                    _queueProducer.PublishMessage(moduleMessage);
                    
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