using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlantCare.API.Services.Requests;

[Route("api/plants/[action]")]
[ApiController]
public class PlantController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PlantController> _logger;

    public PlantController(IMediator mediator, ILogger<PlantController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost(Name = "Create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreatePlantRequest request)
    {
        _logger.LogInformation("Create controller method start processing");
        var result = await _mediator.Send(request);
        _logger.LogInformation("Create controller method ends processing");
        return result.ToOk();
    }

    [HttpDelete(Name = "Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] int Id)
    {
        _logger.LogInformation("Delete controller method start processing");
        var deletePlantRequest = new DeletePlantRequest() { Id = Id };
        var result = await _mediator.Send(deletePlantRequest);
        _logger.LogInformation("Delete controller method ends processing");
        return result.ToOk();
    }

    [HttpPost(Name = "Edit")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Edit(EditPlantRequest request)
    {
        _logger.LogInformation("Edit controller method start processing");
        var result = await _mediator.Send(request);
        _logger.LogInformation("Edit controller method ends processing");
        return result.ToOk();
    }

    [HttpGet(Name = "Get")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPlant))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Get(GetPlantRequest request)
    {
        _logger.LogInformation("Get controller method start processing");
        var result = await _mediator.Send(request);
        _logger.LogInformation("Get controller method ends processing");
        return result.ToOk();
    }
}