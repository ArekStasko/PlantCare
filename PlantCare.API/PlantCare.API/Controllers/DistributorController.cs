using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Distributor;
using PlantCare.Domain.Dto;

namespace PlantCare.API.Controllers;

[Route("api/distributer/")]
[ApiController]
public class DistributorController(
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator,
    ILogger<ModuleController> logger)
    : ControllerAuth(httpContextAccessor, logger)
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public ValueTask<IActionResult> Get()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public ValueTask<IActionResult> Get([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Add(CreateDistributorRequest request)
    {
        AddDistributorCommand command = new AddDistributorCommand()
        {
            Name = request.Name,
            UserId = UserId
        };
        var result = await _mediator.Send(command);
        return result.ToOk();
    }
    
    [HttpPost("{id}/{plantId}/water-supply")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public ValueTask<IActionResult> RunWaterSupply()
    {
        throw new NotImplementedException();
    }
}