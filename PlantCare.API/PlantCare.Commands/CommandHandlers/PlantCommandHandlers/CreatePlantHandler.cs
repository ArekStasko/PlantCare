using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Plant;
using PlantCare.Domain.Models.Plant;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;
using Plant = PlantCare.MessageBroker.Messages.Plant;

namespace PlantCare.Commands.CommandHandlers.PlantCommandHandlers;

public class CreatePlantHandler : IRequestHandler<CreatePlantCommand, Result<bool>>
{
    private readonly IWritePlantRepository _plantRepository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<Plant> _queueProducer;
    private readonly ILogger<CreatePlantHandler> _logger;

    public CreatePlantHandler(IWritePlantRepository plantRepository, IMapper mapper, IQueueProducer<Plant> queueProducer, ILogger<CreatePlantHandler> logger)
    {
        _plantRepository = plantRepository;
        _logger = logger;
        _queueProducer = queueProducer;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(CreatePlantCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("AddPlantHandler handles request");
            IPlant plantToCreate = _mapper.Map<Domain.Models.Plant.Plant>(command);
            var result = await _plantRepository.Create(plantToCreate);
            return result.Match(succ =>
            {
                if (succ)
                {
                    _logger.LogInformation("Operation succesfully completed");
                    return new Result<bool>(true);
                }

                _logger.LogInformation("Something went wrong");
                return new Result<bool>(new Exception("Something went wrong"));
            }, err =>
            {
                _logger.LogError("Error has occured during CreatePlantRequest handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in CreatePlantHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}