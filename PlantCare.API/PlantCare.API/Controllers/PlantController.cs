using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Plant;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Queries.Plant;
using PlantCare.Queries.Responses.Plants;

namespace PlantCare.API.Controllers;

[Route("api/plants")]
[ApiController]
public class PlantController : ControllerAuth
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlantController> _logger;

    public PlantController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IMapper mapper, ILogger<PlantController> logger) : base(httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreatePlantCommand command)
    {
        _logger.LogInformation("Create plant controller method start processing");
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create plant controller method ends processing");
        return result.ToOk();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] int id)
    {
        _logger.LogInformation("Delete plant controller method start processing");
        var deletePlantCommand = _mapper.Map<DeletePlantCommand>(id);
        deletePlantCommand.UserId = UserId;
        var result = await _mediator.Send(deletePlantCommand);
        _logger.LogInformation("Delete plant controller method ends processing");
        return result.ToOk();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Update(UpdatePlantCommand command)
    {
        _logger.LogInformation("Edit plant controller method start processing");
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        _logger.LogInformation("Edit plant controller method ends processing");
        return result.ToOk();
    }

    [HttpGet("/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPlantResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetById(int id)
    {
     
        var getPlantQuery = _mapper.Map<GetPlantQuery>(id);
        getPlantQuery.UserId = UserId;
        _logger.LogInformation("Get plant controller method start processing");
        var result = await _mediator.
            Send(getPlantQuery);
        _logger.LogInformation("Get plant controller method ends processing");
        return result.ToOk();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPlantResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get()
    {
        _logger.LogInformation("GetAll plant controller method start processing");
        var getPlantsQuery = new GetPlantsQuery()
        {
            UserId = UserId
        };
        var result = await _mediator.Send(getPlantsQuery);
        _logger.LogInformation("GetAll plant controller method ends processing");
        return result.ToOk();
    }
}