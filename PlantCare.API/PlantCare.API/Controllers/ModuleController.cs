using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Module;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.Plant;
using GetModulesQuery = PlantCare.Queries.Queries.Module.GetModulesQuery;

namespace PlantCare.API.Controllers;

[Route("api/modules")]
[ApiController]
public class ModuleController : ControllerAuth
{
    private readonly IMediator _mediator;
    private readonly ILogger<PlaceController> _logger;

    public ModuleController(IHttpContextAccessor httpContextAccessor, IMediator mediator, ILogger<PlaceController> logger) : base(httpContextAccessor)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [Route("/status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Status(SetModuleStatusCommand command)
    {
        _logger.LogInformation("Set Module Status controller method start processing");
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        _logger.LogInformation("Set Module Status controller method ends processing");
        return result.ToOk();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreateModuleCommand command)
    {
        _logger.LogInformation("Create module controller method start processing");
        command.UserId = UserId;
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create module controller method ends processing");
        return result.ToOk();
    }
    
    [HttpGet]
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