using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;
using PlantCare.API.Services.Requests.HumidityMeasurementCommands;

namespace PlantCare.API.Services.CommandHandlers.HumidityMeasurementCommandHandlers;

public class AddHumidityMeasurementCommandHandler : IRequestHandler<AddHumidityMeasurementCommand, Result<bool>>
{
    private readonly IHumidityMeasurementRepository _repository;
    private readonly ILogger<AddHumidityMeasurementCommandHandler> _logger;
    private readonly IMapper _mapper;

    public AddHumidityMeasurementCommandHandler(IHumidityMeasurementRepository repository, ILogger<AddHumidityMeasurementCommandHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Result<bool>> Handle(AddHumidityMeasurementCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("AddHumidityMeasurementCommandHandler start processing");
            var humidityMeasurement = _mapper.Map<IHumidityMeasurement>(request);
            var result = await _repository.Add(humidityMeasurement);
            return result.Match(succ =>
            {
                if (succ)
                {
                    _logger.LogInformation("Successfully added humidity measurement");
                    return new Result<bool>(true);
                }

                _logger.LogInformation("Something went wrong");
                return new Result<bool>(new Exception("Something went wrong"));
            }, err =>
            {
                _logger.LogError("Error has occured during AddHumidityMeasurementCommandHandler handling: {exception}", err.Message);
                return new Result<bool>(false);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in AddHumidityMeasurementCommandHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}