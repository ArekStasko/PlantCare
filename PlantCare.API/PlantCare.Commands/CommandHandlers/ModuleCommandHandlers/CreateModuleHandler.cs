using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Module;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Commands.CommandHandlers.ModuleCommandHandlers;

public class CreateModuleHandler(
    IWriteModuleRepository repository,
    IQueueProducer<Module> producer,
    ILogger<CreateModuleHandler> logger)
    : IRequestHandler<CreateModuleCommand, Result<int>>
{

    public async Task<Result<int>> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.Add(request.UserId);
            return result.Match(succ =>
            {
                var moduleDto = new ModuleDto()
                {
                    Id = succ,
                    UserId = request.UserId,
                    IsMonitoring = false,
                };
                var moduleMessage = new Module
                {
                    Action = ActionType.Add,
                    ModuleData = moduleDto,
                };
                producer.PublishMessage(moduleMessage);
                return new Result<int>(succ);
            }, err =>
            {
                logger.LogError("CreateModuleHandler error: {err}", err);
                return new Result<int>(err);
            });
        }
        catch (Exception e)
        {
            logger.LogError("CreateModuleHandler error: {err}", e);
            return new Result<int>(e);
        }
    }
}