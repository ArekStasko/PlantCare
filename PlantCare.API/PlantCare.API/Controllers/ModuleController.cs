using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Module;
using PlantCare.Domain.Models.Plant;
using GetModulesQuery = PlantCare.Queries.Queries.Module.GetModulesQuery;

namespace PlantCare.API.Controllers;

[Route("api/v1/modules/[action]")]
[ApiController]
public class ModuleController : ControllerAuth
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceController> _logger;

    public ModuleController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IMapper mapper, ILogger<PlaceController> logger) : base(httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost(Name = "[controller]/add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Add()
    {
        _logger.LogInformation("Create module controller method start processing");
        var command = new AddModuleCommand()
        {
            UserId = UserId
        };
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create module controller method ends processing");
        return result.ToOk();
    }

    [HttpDelete(Name = "[controller]/delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] Guid id)
    {
        _logger.LogInformation("Delete module controller method start processing");
        var deletePlaceCommand = _mapper.Map<DeleteModuleCommand>(id);
        deletePlaceCommand.UserId = UserId;
        var result = await _mediator.Send(deletePlaceCommand);
        _logger.LogInformation("Delete module controller method ends processing");
        return result.ToOk();
    }

    [HttpPost(Name = "[controller]/update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Update(UpdateModuleCommand command)
    {
        _logger.LogInformation("Edit module controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Edit module controller method ends processing");
        return result.ToOk();
    }

    [HttpGet(Name = "[controller]/get")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IPlant>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get()
    {
        _logger.LogInformation("GetAll modules controller method start processing");
        var getModulesQuery = new GetModulesQuery()
        {
            UserId = UserId
        };
        var result = await _mediator.Send(getModulesQuery);
        _logger.LogInformation("GetAll modules controller method ends processing");
        return result.ToOk();
    }
}