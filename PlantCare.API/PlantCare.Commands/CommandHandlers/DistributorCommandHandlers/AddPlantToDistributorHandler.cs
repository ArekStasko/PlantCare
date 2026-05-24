using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Distributor;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Commands.CommandHandlers.DistributorCommandHandlers;

public class AddPlantToDistributorHandler(
    IWritePlantRepository repository,
    IQueueProducer<Plant> producer,
    ILogger<AddPlantToDistributorHandler> logger
    ) : IRequestHandler<AddPlantToDistributorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AddPlantToDistributorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var plant = await repository.AddDistributor(request.PlantId, request.DistributorId, request.UserId);
            return plant.Match(succ =>
            {
                var plantDistributorDto = new PlantDto()
                {
                    DistributorId = request.DistributorId,
                    Id = request.PlantId,
                    UserId = request.UserId
                };

                var plantDistributorMessage = new Plant()
                {
                    Action = ActionType.AddPlantDistributor,
                    PlantData = plantDistributorDto
                };
                
                producer.PublishMessage(plantDistributorMessage);
                return new Result<bool>(succ);
            }, err =>
            {
                logger.LogError("Something went wrong while adding a plant to distributor: {err}", err);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while adding a plant to distributor: {e}", e);
            return new Result<bool>(e);
        }
    }
}