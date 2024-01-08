using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Abstraction.Commands.Place;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Commands.CommandHandlers.PlaceCommandHandlers;

public class CreatePlaceHandler : IRequestHandler<CreatePlaceCommand, Result<bool>>
{
    private readonly IWritePlaceRepository _placeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePlaceHandler> _logger;

    public CreatePlaceHandler(IWritePlaceRepository placeRepository, IMapper mapper, ILogger<CreatePlaceHandler> logger)
    {
        _placeRepository = placeRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(CreatePlaceCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("CreatePlaceHandler handles request");
            IPlace placeToCreate = _mapper.Map<Place>(command);
            var result = await _placeRepository.Create(placeToCreate);
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