using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.Services.Queries.ModuleQueries;
using PlantCare.API.Services.Requests.ModuleCommands;

namespace PlantCare.API.Controllers;

[Route("api/modules/[action]")]
[ApiController]
public class ModuleController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceController> _logger;

    public ModuleController(IMediator mediator, IMapper mapper, ILogger<PlaceController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost(Name = "[controller]/Add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(AddModuleCommand command)
    {
        _logger.LogInformation("Create module controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Create module controller method ends processing");
        return result.ToOk();
    }

    [HttpDelete(Name = "[controller]/Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] int id)
    {
        _logger.LogInformation("Delete module controller method start processing");
        var deletePlaceCommand = _mapper.Map<DeleteModuleCommand>(id);
        var result = await _mediator.Send(deletePlaceCommand);
        _logger.LogInformation("Delete module controller method ends processing");
        return result.ToOk();
    }

    [HttpPost(Name = "[controller]/Update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Update(UpdateModuleCommand command)
    {
        _logger.LogInformation("Edit module controller method start processing");
        var result = await _mediator.Send(command);
        _logger.LogInformation("Edit module controller method ends processing");
        return result.ToOk();
    }

    [HttpGet(Name = "[controller]/GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IPlant>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetAll()
    {
        _logger.LogInformation("GetAll modules controller method start processing");
        var result = await _mediator.Send(new GetModuleQuery());
        _logger.LogInformation("GetAll modules controller method ends processing");
        return result.ToOk();
    }
}