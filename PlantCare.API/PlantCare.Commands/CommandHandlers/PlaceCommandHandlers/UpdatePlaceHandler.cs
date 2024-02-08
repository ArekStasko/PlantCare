using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Place;
using PlantCare.Domain.Models.Place;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.Interfaces.WriteRepositories;

namespace PlantCare.Commands.CommandHandlers.PlaceCommandHandlers;

public class UpdatePlaceHandler : IRequestHandler<UpdatePlaceCommand, Result<bool>>
{
    private readonly IWritePlaceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<PlaceMessage> _queueProducer;
    private readonly ILogger<UpdatePlaceHandler> _logger;

    public UpdatePlaceHandler(IWritePlaceRepository repository, IMapper mapper, IQueueProducer<PlaceMessage> queueProducer, ILogger<UpdatePlaceHandler> logger)
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
            IPlace placeToEdit = _mapper.Map<Place>(command);
            var result = await _repository.Update(placeToEdit);
            return result.Match(succ =>
            {
                if (succ)
                {
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