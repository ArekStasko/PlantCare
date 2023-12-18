using Microsoft.AspNetCore.SignalR;
using PlantCare.API.Services.Responses;

namespace PlantCare.API.SignalR.Hubs;

public class CurrentMoistureHub : Hub
{
    public async Task<GetCurrentMoistureResponse> GetCurrentMoisture(Guid moduleId)
    {
    }
}