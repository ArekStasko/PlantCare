using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Distributor;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Commands.CommandHandlers.DistributorCommandHandlers;

public class CreateDistributorHandler(
        IWriteDistributorRepository repository,
        IQueueProducer<Distributor> producer,
        ILogger<CreateDistributorHandler> logger
    ) : IRequestHandler<CreateDistributorCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateDistributorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.Create(request.UserId, request.Name);
            int id = result.Match(succ => succ, err =>
            {
                logger.LogError("Something went wrong while adding new distributor");
                throw err;
            });
            
            var distributorDto = new DistributorDto()
            {
                Id = id,
                UserId = request.UserId,
                Name = request.Name
            };

            var distributorMessage = new Distributor()
            {
                Action = ActionType.Add,
                DistributorDto = distributorDto
            };

            producer.PublishMessage(distributorMessage);
            return new Result<int>(id);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while executing ");
            return new Result<int>(e);
        }
    }
}