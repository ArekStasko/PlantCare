using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.API.Place;
using PlantCare.Commands.Abstraction.Commands.HumidityMeasurements;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Queries.Abstraction.Queries.HumidityMeasurements;

namespace PlantCare.API.HumidityMeasurements;

[Route("api/v1/humidity-measurements/[action]")]
[ApiController]

public class HumidityMeasurementsController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceController> _logger;

    public HumidityMeasurementsController(IMediator mediator, IMapper mapper, ILogger<PlaceController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost(Name = "[controller]/add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Add(AddHumidityMeasurementCommand command)
    {
        _logger.LogInformation("Create humidity measurement controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create humidity measurement method ends processing");
        return result.ToOk();
    }

    [HttpGet(Name = "[controller]/get")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<IHumidityMeasurement>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get([FromQuery] Guid id, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        _logger.LogInformation("Get humidity measurements controller method start processing");
        var getHumidityMeasurementsQuery = new GetHumidityMeasurementQuery()
        {
            Id = id,
            FromDate = fromDate,
            ToDate = toDate,
        };
        var result = await _mediator.Send(getHumidityMeasurementsQuery);
        _logger.LogInformation("Get humidity measurements controller method ends processing");
        return result.ToOk();
    }
}