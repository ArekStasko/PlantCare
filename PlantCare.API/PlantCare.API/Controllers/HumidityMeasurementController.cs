using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.Services.Queries.HumidityMeasurementsQueries;
using PlantCare.API.Services.Requests.HumidityMeasurementCommands;

namespace PlantCare.API.Controllers;

[Route("api/humidity-measurements/[action]")]
[ApiController]
public class HumidityMeasurementController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceController> _logger;

    public HumidityMeasurementController(IMediator mediator, IMapper mapper, ILogger<PlaceController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost(Name = "[controller]/Add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(AddHumidityMeasurementCommand command)
    {
        _logger.LogInformation("Create humidity measurement controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create humidity measurement method ends processing");
        return result.ToOk();
    }

    [HttpGet(Name = "[controller]/Get")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<IHumidityMeasurement>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get([FromQuery] int id)
    {
        _logger.LogInformation("Get humidity measurements controller method start processing");
        var getHumidityMeasurementsQuery = _mapper.Map<GetHumidityMeasurementQuery>(id);
        var result = await _mediator.Send(getHumidityMeasurementsQuery);
        _logger.LogInformation("Get humidity measurements controller method ends processing");
        return result.ToOk();
    }
}