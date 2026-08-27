using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Distributor;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;
namespace PlantCare.Commands.CommandHandlers.DistributorCommandHandlers;

public class RemoveWaterSupplyHandler(
        IWriteDistributorRepository repository,
        IQueueProducer<WaterSupply> producer,
        ILogger<CreateDistributorHandler> logger
    ) : IRequestHandler<DeleteWaterSupplyCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteWaterSupplyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.RemoveWaterSupply(request.DistributorId, request.PlantId);
            _ = result.Match(succ =>
            {
                if (!succ)
                {
                    logger.LogError("Something went wrong in repository remove water supply method");
                    return new Result<bool>(false);
                }

                return succ;
            }, err =>
            {
                logger.LogError("Something went wrong while running remove water supply");
                throw err;
            });
            
            var waterSupplyDto = new Domain.Models.Distributor.WaterSupply()
            {
                DistributorId = request.DistributorId,
                PlantId = request.PlantId,
            };

            var waterSupplyMessage = new WaterSupply()
            {
                Action = ActionType.Delete,
                WaterSupplyDto = waterSupplyDto
            };

            producer.PublishMessage(waterSupplyMessage);
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while executing remove water supply method: {e}", e);
            return new Result<bool>(e);
        }
    }
}