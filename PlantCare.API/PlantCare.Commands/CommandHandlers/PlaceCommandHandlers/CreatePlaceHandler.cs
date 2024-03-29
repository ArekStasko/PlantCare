using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Place;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.Place;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;
using Place = PlantCare.MessageBroker.Messages.Place;

namespace PlantCare.Commands.CommandHandlers.PlaceCommandHandlers;

public class CreatePlaceHandler : IRequestHandler<CreatePlaceCommand, Result<bool>>
{
    private readonly IWritePlaceRepository _placeRepository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<Place> _queueProducer;
    private readonly ILogger<CreatePlaceHandler> _logger;

    public CreatePlaceHandler(IWritePlaceRepository placeRepository, IMapper mapper, IQueueProducer<Place> queueProducer, ILogger<CreatePlaceHandler> logger)
    {
        _placeRepository = placeRepository;
        _mapper = mapper;
        _queueProducer = queueProducer;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(CreatePlaceCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("CreatePlaceHandler handles request");
            IPlace placeToCreate = _mapper.Map<Domain.Models.Place.Place>(command);
            
            var result = await _placeRepository.Create(placeToCreate);
            return result.Match(succ =>
            {
                    var placeDto = _mapper.Map<PlaceDto>(placeToCreate);
                    placeDto.Id = succ;
                    var placeMessage = new Place()
                    {
                        Action = ActionType.Add,
                        PlaceData = placeDto
                    };
                    _queueProducer.PublishMessage(placeMessage);
                    
                    _logger.LogInformation("Operation succesfully completed");
                    return new Result<bool>(true);
            }, err =>
            {
                _logger.LogError("Error has occured during CreatePlaceHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in CreatePlaceHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}