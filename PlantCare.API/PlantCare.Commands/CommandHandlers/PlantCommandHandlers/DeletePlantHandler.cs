using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Plant;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Commands.CommandHandlers.PlantCommandHandlers;

public class DeletePlantHandler : IRequestHandler<DeletePlantCommand, Result<bool>>
{
    private readonly IWritePlantRepository _plantRepository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<Plant> _queueProducer;
    private readonly ILogger<DeletePlantHandler> _logger;
    
    public DeletePlantHandler(IWritePlantRepository plantRepository, IMapper mapper, IQueueProducer<Plant> queueProducer, ILogger<DeletePlantHandler> logger)
    {
        _plantRepository = plantRepository;
        _mapper = mapper;
        _queueProducer = queueProducer;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Handle(DeletePlantCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeletePlantHandler handles request");
            var result = await _plantRepository.Delete(command.Id, command.UserId);
            return result.Match(succ =>
            {
                if (succ)
                {
                    var plantDto = new Domain.Models.Plant.Plant() { Id = command.Id, UserId = command.UserId};
                    var plantMessage = new Plant()
                    {
                        Action = ActionType.Delete,
                        PlantData = _mapper.Map<PlantDto>(plantDto)
                    };
                    _queueProducer.PublishMessage(plantMessage);
                    
                    _logger.LogInformation("Operation Successfully completed");
                    return new Result<bool>(true);
                }

                return new Result<bool>(false);
            }, err =>
            {
                _logger.LogError("Error has occured during DeletePlantHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in DeletePlantHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}