using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Module;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Commands.CommandHandlers.ModuleCommandHandlers;

public class AddModuleHandler : IRequestHandler<AddModuleCommand, Result<Guid>>
{
    private readonly IWriteModuleRepository _repository;
    private readonly IQueueProducer<Module> _queueProducer;
    private readonly ILogger<AddModuleHandler> _logger;

    public AddModuleHandler(IWriteModuleRepository repository, IQueueProducer<Module> queueProducer, ILogger<AddModuleHandler> logger)
    {
        _repository = repository;
        _queueProducer = queueProducer;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(AddModuleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("AddModuleHandler start processing");
            Guid newId = Guid.NewGuid();
            var result = await _repository.Add(newId);
            return result.Match(succ =>
            {
                _logger.LogInformation("Successfully added module with {id} id", newId);
                return new Result<Guid>(succ);
            }, err =>
            {
                _logger.LogError("Error has occured during AddModuleHandler handling: {exception}", err.Message);
                return new Result<Guid>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in AddModuleHandler: {exception}", e.Message);
            return new Result<Guid>(e);
        }
    }
}