using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Module;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;
using Module = PlantCare.MessageBroker.Messages.Module;

namespace PlantCare.Commands.CommandHandlers.ModuleCommandHandlers;

public class SetModuleStatusHandler(
    IWriteModuleRepository repository,
    IQueueProducer<Module> producer,
    ILogger<SetModuleStatusHandler> logger)
    : IRequestHandler<SetModuleStatusCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SetModuleStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.UserId == null)
            {
                logger.LogError("UserId cannot be null");
                return new Result<bool>(false);
            }
            
            int userId = (int)request.UserId;
            var result = await repository.UpdateStatus(userId, request.ModuleId, request.Status);
            return result.Match(succ =>
            {
                var moduleDto = new ModuleDto()
                {
                    Id = request.ModuleId,
                    UserId = userId,
                    IsMonitoring = request.Status
                };
                var moduleMessage = new Module()
                {
                    Action = ActionType.Update,
                    ModuleData = moduleDto,
                };
                producer.PublishMessage(moduleMessage);
                return true;
            }, err =>
            {
                logger.LogError("SetModuleStatusHandler error: {err}", err);
                return false;
            });
        }
        catch (Exception e)
        {
            logger.LogError("CreateModuleHandler error: {err}", e);
            return new Result<bool>(e);
        }
    }
}