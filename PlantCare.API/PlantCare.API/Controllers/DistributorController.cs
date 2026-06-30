using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Commands.Commands.Distributor;
using PlantCare.Domain.Dto;
using PlantCare.Queries.Queries.Distributor;
using PlantCare.Queries.Responses.Distributor;

namespace PlantCare.API.Controllers;

[Route("api/distributor/")]
[ApiController]
public class DistributorController(
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator,
    ILogger<ModuleController> logger)
    : ControllerAuth(httpContextAccessor, logger)
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<Distributor>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get()
    {
        GetDistributorsQuery query = new()
        {
            UserId = UserId
        };
        
        var result = await mediator.Send(query);
        return result.ToOk();
    }
    
    [HttpGet("{id}")]
    [EndpointName("GetDistributor")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Distributor))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get([FromRoute] int id)
    {
        GetDistributorByIdQuery query = new GetDistributorByIdQuery()
        {
            UserId = UserId,
            Id = id
        };
        var result = await mediator.Send(query);
        return result.ToOk();
    }
    
    [HttpPost]
    [EndpointName("CreateDistributor")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreateDistributorRequest request)
    {
        CreateDistributorCommand command = new CreateDistributorCommand()
        {
            Name = request.Name,
            UserId = UserId
        };
        var result = await mediator.Send(command);
        return result.ToOk();
    }

    [HttpPost("{distributorId}/{plantId}/add")]
    [EndpointName("AddPlantToDistributor")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> AddPlantToDistributor([FromRoute] int distributorId, [FromRoute] int plantId)
    {
        AddPlantToDistributorCommand command = new()
        {
            UserId = UserId,
            DistributorId = distributorId,
            PlantId = plantId
        };
        var result = await mediator.Send(command);
        return result.ToOk();
    }
    
    [HttpPost("{id:int}/{plantId:int}/water-supply")]
    [EndpointName("WaterSupply")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public ValueTask<IActionResult> RunWaterSupply([FromRoute] int Id, [FromRoute] int PlantId)
    {
        throw new NotImplementedException();
    }
}