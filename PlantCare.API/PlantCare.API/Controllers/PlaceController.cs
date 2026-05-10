using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Place;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Queries.Place;
using PlantCare.Queries.Responses.HumidityMeasurements;
using PlantCare.Queries.Responses.Place;
using Place = PlantCare.Queries.Responses.Place.Place;

namespace PlantCare.API.Controllers;

[Route("api/places/")]
[ApiController]
public class PlaceController : ControllerAuth
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceController> _logger;

    public PlaceController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IMapper mapper, ILogger<PlaceController> logger) : base(httpContextAccessor, logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreatePlaceCommand command)
    {
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        return result.ToOk();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] int id)
    {
        var deletePlaceCommand = _mapper.Map<DeletePlaceCommand>(id);
        deletePlaceCommand.UserId = UserId;
        var result = await _mediator.Send(deletePlaceCommand);
        return result.ToOk();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Update(UpdatePlaceCommand command)
    {
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        return result.ToOk();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Place>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get()
    {
        var getPlacesQuery = new GetPlacesQuery()
        {
            UserId = UserId
        };
        var result = await _mediator.Send(getPlacesQuery);
        return result.ToOk();
    }
    
    [HttpGet("{id:int}/plants/humidity-status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PlantHumidityStatus>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get([FromRoute] int id)
    {
        var getHumidityStatusForPlacePlantsQuery = new GetHumidityStatusForPlacePlantsQuery()
        {
            UserId = UserId,
            Id = id
        };
        var result = await _mediator.Send(getHumidityStatusForPlacePlantsQuery);
        return result.ToOk();
    }
}