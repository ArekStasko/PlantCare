namespace PlantCare.API.Services.Handlers;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Requests;
using Serilog;
using ILogger = Serilog.ILogger;
public class DeletePlantHandler : IRequestHandler<DeletePlantCommand, Result<bool>>
{
    private readonly IPlantRepository _plantRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeletePlantHandler> _logger;
    
    public DeletePlantHandler(IPlantRepository plantRepository, IMapper mapper, ILogger<DeletePlantHandler> logger)
    {
        _plantRepository = plantRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Handle(DeletePlantCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeletePlantHandler handles request");
            var result = await _plantRepository.Delete(command.Id);
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
                _logger.LogError("Error has occured during DeletePlantHandler handling: {exception}", err.Message);
                return new Result<bool>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in DeletePlantHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}