using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Plant;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.Plant;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;
using Plant = PlantCare.MessageBroker.Messages.Plant;

namespace PlantCare.Commands.CommandHandlers.PlantCommandHandlers;

public class UpdatePlantHumidityValuesHandler : IRequestHandler<UpdatePlantHumidityValues, Result<bool>>
{
    private readonly IWritePlantRepository _repository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<Plant> _queueProducer;
    private readonly ILogger<UpdatePlantHandler> _logger;
    
    public UpdatePlantHumidityValuesHandler(IWritePlantRepository repository, IMapper mapper, IQueueProducer<Plant> queueProducer, ILogger<UpdatePlantHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _queueProducer = queueProducer;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(UpdatePlantHumidityValues command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.UpdateHumidityValues(command.PlantId, command.minHumidity, command.maxHumidity);
            return result.Match(succ =>
            {
                if (succ)
                {
                    
                }
                return succ;
            }, err =>
            {
                _logger.LogError($"Failed to update humidity values: {err}", err);
                return new Result<bool>(err);
            });

        }
        catch (Exception e)
        {
            _logger.LogError("Update plant humidity values failed with error: {e}", e);
            return new Result<bool>(e);
        }
    }
}