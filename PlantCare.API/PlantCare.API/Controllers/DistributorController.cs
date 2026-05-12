using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public ValueTask<IActionResult> GetDistributors()
    {
        
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public ValueTask<IActionResult> GetDistributor()
    {
        
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public ValueTask<IActionResult> AddDistributor()
    {

    }
    
    [HttpPost("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public ValueTask<IActionResult> AddPlantToDistributor()
    {

    }
    
    [HttpPost("{id}/{plantId}/water-supply")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public ValueTask<IActionResult> RunWaterSupply()
    {
        
    }
}