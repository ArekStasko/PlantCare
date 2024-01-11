using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Abstraction.Commands.Plant;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Abstraction.Queries.Plant;

namespace PlantCare.Controllers.Plant;

[Route("api/v1/plants/[action]")]
[ApiController]
public class PlantController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlantController> _logger;

    public PlantController(IMediator mediator, IMapper mapper, ILogger<PlantController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost(Name = "[controller]/create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreatePlantCommand command)
    {
        _logger.LogInformation("Create plant controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create plant controller method ends processing");
        return result.ToOk();
    }

    [HttpDelete(Name = "[controller]/delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] int id)
    {
        _logger.LogInformation("Delete plant controller method start processing");
        var deletePlantCommand = _mapper.Map<DeletePlantCommand>(id);
        var result = await _mediator.Send(deletePlantCommand);
        _logger.LogInformation("Delete plant controller method ends processing");
        return result.ToOk();
    }

    [HttpPost(Name = "[controller]/update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Update(UpdatePlantCommand command)
    {
        _logger.LogInformation("Edit plant controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Edit plant controller method ends processing");
        return result.ToOk();
    }

    [HttpGet(Name = "[controller]/get")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPlant))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get([FromQuery] int id)
    {
        var getPlantQuery = _mapper.Map<GetPlantQuery>(id);
        _logger.LogInformation("Get plant controller method start processing");
        var result = await _mediator.
            Send(getPlantQuery);
        _logger.LogInformation("Get plant controller method ends processing");
        return result.ToOk();
    }
    
    [HttpGet(Name = "[controller]/get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IPlant>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetAll()
    {
        _logger.LogInformation("GetAll plant controller method start processing");
        var result = await _mediator.Send(new GetPlantsQuery());
        _logger.LogInformation("GetAll plant controller method ends processing");
        return result.ToOk();
    }
}