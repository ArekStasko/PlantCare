using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Module;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Queries.Module;
using PlantCare.Queries.Responses.Module;
using GetModulesQuery = PlantCare.Queries.Queries.Module.GetModulesQuery;

namespace PlantCare.API.Controllers;

[Route("api/modules/")]
[ApiController]
public class ModuleController : ControllerAuth
{
    private readonly IMediator _mediator;

    public ModuleController(IHttpContextAccessor httpContextAccessor, IMediator mediator, ILogger<PlaceController> logger) : base(httpContextAccessor, logger)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreateModuleRequest request)
    {
        CreateModuleCommand command = new CreateModuleCommand()
        {
            Name = request.Name,
            UserId = UserId
        };
        var result = await _mediator.Send(command);
        return result.ToOk();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetModuleResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get()
    {
        var getModulesQuery = new GetModulesQuery()
        {
            UserId = UserId
        };
        var result = await _mediator.Send(getModulesQuery);
        return result.ToOk();
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetModuleResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetModuleById(int id)
    {
        var getModulesQuery = new GetModuleByIdQuery()
        {
            UserId = UserId,
            ModuleId = id
        };
        var result = await _mediator.Send(getModulesQuery);
        return result.ToOk();
    }
}