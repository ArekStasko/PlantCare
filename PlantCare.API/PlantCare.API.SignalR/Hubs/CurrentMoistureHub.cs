using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PlantCare.API.Services.Queries.ModuleQueries;
using PlantCare.API.Services.Responses;

namespace PlantCare.API.SignalR.Hubs;

public class CurrentMoistureHub : Hub
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CurrentMoistureHub> _logger;

    public CurrentMoistureHub(IMediator mediator, IMapper mapper, ILogger<CurrentMoistureHub> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async ValueTask<IActionResult> GetCurrentMoisture(Guid moduleId)
    {
        _logger.LogInformation("CurrentMoisture Hub start processing");
        var getCurrentMoistureQuery = _mapper.Map<GetCurrentMositureQuery>(moduleId);
        var result = await _mediator.Send(getCurrentMoistureQuery);
        _logger.LogInformation("CurrentMoisture Hub ends processing");
        return result.ToOk();
    }
}