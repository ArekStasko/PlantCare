namespace PlantCare.API.Services.Handlers;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Requests;

public class CreatePlantHandler : IRequestHandler<CreatePlantCommand, Result<bool>>
{
    private readonly IPlantRepository _plantRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePlantHandler> _logger;

    public CreatePlantHandler(IPlantRepository plantRepository, IMapper mapper, ILogger<CreatePlantHandler> logger)
    {
        _plantRepository = plantRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(CreatePlantCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("AddPlantHandler handles request");
            IPlant plantToCreate = _mapper.Map<Plant>(command);
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