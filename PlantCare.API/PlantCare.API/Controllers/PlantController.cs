using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Plant;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Queries.Plant;
using PlantCare.Queries.Responses.Plants;
using Plant = PlantCare.Queries.Responses.Plants.Plant;

namespace PlantCare.API.Controllers;

[Route("api/plants/")]
[ApiController]
public class PlantController : ControllerAuth
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PlantController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IMapper mapper, ILogger<PlantController> logger) : base(httpContextAccessor, logger)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreatePlantCommand command)
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
        var deletePlantCommand = _mapper.Map<DeletePlantCommand>(id);
        deletePlantCommand.UserId = UserId;
        var result = await _mediator.Send(deletePlantCommand);
        return result.ToOk();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Update(UpdatePlantCommand command)
    {
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        return result.ToOk();
    }
    
    [HttpPut("{id:int}/set-humidity-values")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Update([FromRoute] int id, UpdatePlantHumidityValues command)
    {
        command.UserId = UserId;
        command.PlantId = id;
        var result = await _mediator.Send(command);
        return result.ToOk();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Plant))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetPlantById(int id)
    {
        var getPlantQuery = _mapper.Map<GetPlantQuery>(id);
        getPlantQuery.UserId = UserId;
        var result = await _mediator.
            Send(getPlantQuery);
        return result.ToOk();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Plant>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get()
    {
        var getPlantsQuery = new GetPlantsQuery()
        {
            UserId = UserId
        };
        var result = await _mediator.Send(getPlantsQuery);
        return result.ToOk();
    }
}