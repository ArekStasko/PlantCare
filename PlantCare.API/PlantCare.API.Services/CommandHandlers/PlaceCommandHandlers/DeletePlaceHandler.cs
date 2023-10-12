namespace PlantCare.API.Services.CommandHandlers.PlaceCommandHandlers;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;
using PlantCare.API.Services.Requests.PlaceCommands;

public class DeletePlaceHandler : IRequestHandler<DeletePlaceCommand, Result<bool>>
{
    private readonly IPlaceRepository _placeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeletePlaceHandler> _logger;

    public DeletePlaceHandler(IPlaceRepository placeRepository, IMapper mapper, ILogger<DeletePlaceHandler> logger)
    {
        _placeRepository = placeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeletePlaceCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeletePlaceHandler handles request");
            var result = await _placeRepository.Delete(command.Id);
            return result.Match(succ =>
            {
                if (succ)
                {
                    _logger.LogInformation("Operation Successfully completed");
                    return new Result<bool>(true);
                }

                return new Result<bool>(false);
            }, err =>
            {
                _logger.LogError("Error has occured during DeletePlaceHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in DeletePlaceHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}