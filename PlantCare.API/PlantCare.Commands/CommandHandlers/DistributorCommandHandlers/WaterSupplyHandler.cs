using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Distributor;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Commands.CommandHandlers.DistributorCommandHandlers;

public class WaterSupplyHandler(
        IWriteDistributorRepository repository,
        IQueueProducer<Distributor> producer,
        ILogger<CreateDistributorHandler> logger
    ) : IRequestHandler<WaterSupplyCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(WaterSupplyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.WaterSupply(request.UserId, request.DistributorId);
            _ = result.Match(succ =>
            {
                if (!succ)
                {
                    logger.LogError("Something went wrong in repository water supply method");
                    return new Result<bool>(false);
                }

                return succ;
            }, err =>
            {
                logger.LogError("Something went wrong while running water supply");
                throw err;
            });
            
            var distributorDto = new DistributorDto()
            {
                Id = request.DistributorId,
                UserId = request.UserId,
            };

            var distributorMessage = new Distributor()
            {
                Action = ActionType.WaterSupply,
                DistributorDto = distributorDto
            };

            producer.PublishMessage(distributorMessage);
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while executing water supply method: {e}", e);
            return new Result<bool>(e);
        }
    }
}