using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PlantCare.API.Services.Queries.ModuleQueries;

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
    
    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Connected to Hub");
        await base.OnConnectedAsync();
    }
    
    public async ValueTask<IActionResult> SendMessage(Guid moduleId)
    {
        _logger.LogInformation("CurrentMoisture Hub start processing");
        var getCurrentMoistureQuery = _mapper.Map<GetCurrentMositureQuery>(moduleId);
        var result = await _mediator.Send(getCurrentMoistureQuery);
        _logger.LogInformation("CurrentMoisture Hub ends processing");
        await Clients.Caller.SendAsync("ReceiveMoisture", result.ToOk());
        return result.ToOk();
    }
}