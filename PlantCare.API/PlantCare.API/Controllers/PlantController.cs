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

    public PlantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "Create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Create(CreatePlantRequest request)
    {
        var result = await _mediator.Send(request);
        return result.ToOk();
    }

    [HttpDelete(Name = "Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Delete([FromQuery] int Id)
    {
        var deletePlantRequest = new DeletePlantRequest() { Id = Id };
        var result = await _mediator.Send(deletePlantRequest);
        return result.ToOk();
    }
}