using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.HumidityMeasurements;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Queries.Queries.HumidityMeasurements;

namespace PlantCare.API.Controllers;

[Route("api/humidity-measurements/")]
[ApiController]
public class HumidityMeasurementController : ControllerAuth
{
    private readonly IMediator _mediator;

    public HumidityMeasurementController(IHttpContextAccessor httpContextAccessor, IMediator mediator, ILogger<PlaceController> logger) : base(httpContextAccessor, logger)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Add(AddHumidityMeasurementCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToOk();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<IHumidityMeasurement>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get([FromQuery] int id, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var getHumidityMeasurementsQuery = new GetHumidityMeasurementQuery()
        {
            Id = id,
            FromDate = fromDate,
            ToDate = toDate,
        };
        var result = await _mediator.Send(getHumidityMeasurementsQuery);
        return result.ToOk();
    }

    [HttpGet("{id:int}/average")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<AverageHumidity>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetAverage(int id, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var getAverageMeasurementsQuery = new GetAverageHumidityMeasurementQuery()
        {
            FromDate = fromDate,
            ToDate = toDate,
            ModuleId = id
        };
        var result = await _mediator.Send(getAverageMeasurementsQuery);
        return result.ToOk();
    }
}