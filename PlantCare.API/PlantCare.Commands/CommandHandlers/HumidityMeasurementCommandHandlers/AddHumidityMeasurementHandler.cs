using AutoMapper;
using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Commands.CommandHandlers.HumidityMeasurementCommandHandlers;

public class AddHumidityMeasurementHandler : IRequestHandler<AddHumidityMeasurementCommand, Result<bool>>
{
    private readonly IWriteHumidityMeasurementRepository _repository;
    private readonly ILogger<AddHumidityMeasurementCommandHandler> _logger;
    private readonly IMapper _mapper;

    public AddHumidityMeasurementCommandHandler(IWriteHumidityMeasurementRepository repository, ILogger<AddHumidityMeasurementCommandHandler> logger, IMapper mapper)
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
            IHumidityMeasurement humidityMeasurement = _mapper.Map<HumidityMeasurement>(request);
            humidityMeasurement.MeasurementDate = DateTime.Now;
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