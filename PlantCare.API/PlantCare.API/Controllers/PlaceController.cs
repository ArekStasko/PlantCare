using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Place;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Queries.Place;

namespace PlantCare.API.Controllers;

[Route("api/v1/places/[action]")]
[ApiController]
public class PlaceController : ControllerAuth
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceController> _logger;

    public PlaceController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IMapper mapper, ILogger<PlaceController> logger) : base(httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost(Name = "[controller]/create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreatePlaceCommand command)
    {
        _logger.LogInformation("Create place controller method start processing");
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create place controller method ends processing");
        return result.ToOk();
    }

    [HttpDelete(Name = "[controller]/delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] int id)
    {
        _logger.LogInformation("Delete place controller method start processing");
        var deletePlaceCommand = _mapper.Map<DeletePlaceCommand>(id);
        deletePlaceCommand.UserId = UserId;
        var result = await _mediator.Send(deletePlaceCommand);
        _logger.LogInformation("Delete place controller method ends processing");
        return result.ToOk();
    }

    [HttpPost(Name = "[controller]/update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Update(UpdatePlaceCommand command)
    {
        _logger.LogInformation("Edit place controller method start processing");
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        _logger.LogInformation("Edit place controller method ends processing");
        return result.ToOk();
    }

    [HttpGet(Name = "[controller]/get")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IPlant>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get()
    {
        _logger.LogInformation("GetAll places controller method start processing");
        var getPlacesQuery = new GetPlacesQuery()
        {
            UserId = UserId
        };
        var result = await _mediator.Send(getPlacesQuery);
        _logger.LogInformation("GetAll places controller method ends processing");
        return result.ToOk();
    }
}