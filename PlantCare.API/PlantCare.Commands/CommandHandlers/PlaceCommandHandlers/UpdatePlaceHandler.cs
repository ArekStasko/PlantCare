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

public class UpdatePlaceHandler : IRequestHandler<UpdatePlaceCommand, Result<bool>>
{
    private readonly IWritePlaceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<Place> _queueProducer;
    private readonly ILogger<UpdatePlaceHandler> _logger;

    public UpdatePlaceHandler(IWritePlaceRepository repository, IMapper mapper, IQueueProducer<Place> queueProducer, ILogger<UpdatePlaceHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _queueProducer = queueProducer;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(UpdatePlaceCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("EditPlaceHandler handles request");
            IPlace placeToUpdate = _mapper.Map<Domain.Models.Place.Place>(command);
            var result = await _repository.Update(placeToUpdate);
            return result.Match(succ =>
            {
                if (succ)
                {
                    var placeMessage = new Place()
                    {
                        Action = ActionType.Update,
                        PlaceData = _mapper.Map<PlaceDto>(placeToUpdate)
                    };
                    _queueProducer.PublishMessage(placeMessage);
                    
                    _logger.LogInformation("Operation successfully completed");
                    return new Result<bool>(succ);
                }

                _logger.LogInformation("Something went wrong");
                return new Result<bool>(false);
            }, err =>
            {
                _logger.LogError("Error has occured during EditPlaceHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in EditPlaceHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}