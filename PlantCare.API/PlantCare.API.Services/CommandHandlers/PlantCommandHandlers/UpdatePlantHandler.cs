namespace PlantCare.API.Services.Handlers;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Requests;

public class UpdatePlantHandler : IRequestHandler<UpdatePlantCommand, Result<bool>>
{
    private readonly IWritePlantRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdatePlantHandler> _logger;

    public UpdatePlantHandler(IWritePlantRepository repository, IMapper mapper, ILogger<UpdatePlantHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(UpdatePlantCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("UpdatePlantHandler handles request");
            IPlant plantToUpdate = _mapper.Map<Plant>(command);
            var result = await _repository.Update(plantToUpdate);
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
                _logger.LogError("Error has occured during UpdatePlantHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in UpdatePlantHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}