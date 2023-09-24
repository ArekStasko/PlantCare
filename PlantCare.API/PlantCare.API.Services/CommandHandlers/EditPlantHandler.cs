using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Requests;

namespace PlantCare.API.Services.Handlers;

public class EditPlantHandler : IRequestHandler<EditPlantCommand, Result<bool>>
{
    private readonly IPlantRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<EditPlantCommand> _logger;

    public EditPlantHandler(IPlantRepository repository, IMapper mapper, ILogger<EditPlantCommand> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(EditPlantCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("EditPlantHandler handles request");
            IPlant plantToEdit = _mapper.Map<Plant>(command);
            var result = await _repository.Edit(plantToEdit);
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
                _logger.LogError("Error has occured during EditPlantHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in EditPlantHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}