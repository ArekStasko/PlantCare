namespace PlantCare.API.Controllers;

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.Services.Queries.PlaceQueries;
using PlantCare.API.Services.Requests.PlaceCommands;

[Route("api/places/[action]")]
[ApiController]
public class PlaceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceController> _logger;

    public PlaceController(IMediator mediator, IMapper mapper, ILogger<PlaceController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost(Name = "[controller]/Create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreatePlaceCommand command)
    {
        _logger.LogInformation("Create place controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create place controller method ends processing");
        return result.ToOk();
    }

    [HttpDelete(Name = "[controller]/Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] int id)
    {
        _logger.LogInformation("Delete place controller method start processing");
        var deletePlaceCommand = _mapper.Map<DeletePlaceCommand>(id);
        var result = await _mediator.Send(deletePlaceCommand);
        _logger.LogInformation("Delete place controller method ends processing");
        return result.ToOk();
    }

    [HttpPost(Name = "[controller]/Edit")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Edit(EditPlaceCommand command)
    {
        _logger.LogInformation("Edit place controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Edit place controller method ends processing");
        return result.ToOk();
    }

    [HttpGet(Name = "[controller]/GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IPlant>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetAll(GetPlacesQuery query)
    {
        _logger.LogInformation("GetAll places controller method start processing");
        var result = await _mediator.Send(query);
        _logger.LogInformation("GetAll places controller method ends processing");
        return result.ToOk();
    }
}