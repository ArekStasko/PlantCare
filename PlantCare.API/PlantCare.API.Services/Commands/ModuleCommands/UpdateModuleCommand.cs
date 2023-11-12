namespace PlantCare.API.Services.Requests.ModuleCommands;

public class UpdateModuleCommand : IHttpPostCommand
{
    public int RequiredMoistureLevel { get; set; }

    public int CriticalMoistureLevel { get; set; }
}