using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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