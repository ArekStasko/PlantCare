using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.HumidityMeasurements;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.Interfaces.WriteRepositories;

namespace PlantCare.Commands.CommandHandlers.HumidityMeasurementCommandHandlers;

public class AddHumidityMeasurementHandler : IRequestHandler<AddHumidityMeasurementCommand, Result<bool>>
{
    private readonly IWriteHumidityMeasurementRepository _repository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<HumidityMeasurementMessage> _consumer;
    private readonly ILogger<AddHumidityMeasurementHandler> _logger;

    public AddHumidityMeasurementHandler(IWriteHumidityMeasurementRepository repository, IMapper mapper, IQueueProducer<HumidityMeasurementMessage> consumer, ILogger<AddHumidityMeasurementHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _consumer = consumer;
        _logger = logger;
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