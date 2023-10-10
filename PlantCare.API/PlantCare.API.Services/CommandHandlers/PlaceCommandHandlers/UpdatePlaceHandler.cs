namespace PlantCare.API.Services.CommandHandlers.PlaceCommandHandlers;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;
using PlantCare.API.Services.Requests.PlaceCommands;

public class UpdatePlaceHandler : IRequestHandler<UpdatePlaceCommand, Result<bool>>
{
    private readonly IPlaceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdatePlaceHandler> _logger;

    public UpdatePlaceHandler(IPlaceRepository repository, IMapper mapper, ILogger<UpdatePlaceHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
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